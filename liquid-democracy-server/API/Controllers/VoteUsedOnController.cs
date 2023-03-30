namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VoteUsedOnController : ControllerBase
{
    readonly IVoteUsedOnRepository _repository;
    readonly IVoteRepository _voteRepository;
    readonly IElectionRepository _electionRepository;
    readonly IUserRepository _userRepository;

    public VoteUsedOnController(IVoteUsedOnRepository repository, IVoteRepository voteRepository, IElectionRepository electionRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _voteRepository = voteRepository;
        _electionRepository = electionRepository;
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/ForCandidate/{providerId}/{electionId}/{candidateId}/{documentId}")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    [ProducesResponseType(403)]
    public async Task<RedirectResult> CreateVoteUsedOnForCandidate(string providerId, int electionId, int candidateId, string documentId)
    {
        var user = await _userRepository.GetByProiverId(providerId);

        if (user == null)
        {
            user = await _userRepository.Create(providerId, DateTime.Now);
        }

        var checkForVote = _voteRepository.checkForExistingVote(user.UserId, electionId);

        if (checkForVote.Result != null)
        {
        return new RedirectResult(url: "http://localhost:3000/Error", permanent: true, preserveMethod: true);
        }

        if (!await verifyRootHash(electionId))
        {
        return new RedirectResult(url: "http://localhost:3000/Error", permanent: true, preserveMethod: true);
        }

        var vote = await _voteRepository.CreateAsync(user.UserId, electionId, documentId);
        await buildNewRootHash(electionId);

        await _repository.CreateVoteUsedOnForCandidate(vote.VoteId, candidateId);

        return new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/VerifyRootHash/{electionId}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<bool> VeirfyElectionByRootHash(int electionId)
    {
        var canBeVerified = await verifyRootHash(electionId);
        return canBeVerified;
    }


    private async Task<bool> verifyRootHash(int electionId)
    {
        var votes = await _voteRepository.ReadFromElectionId(electionId);
        var election = await _electionRepository.GetElectionByIDAsync(electionId);

        if (election == null)
        {
            return false;
        }

        if (votes.Count() == 0)
        {
            return true;
        }

        var RootHashForElection = election.RootHash;
        var documentIds = new List<string>();

        foreach (var vote in votes)
        {
            documentIds.Add(vote.DocumentId);
        }

        MerkleTree merkleTree = new MerkleTree(documentIds);
        if (RootHashForElection == merkleTree.RootHash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private async Task buildNewRootHash(int electionId)
    {
        var votes = await _voteRepository.ReadFromElectionId(electionId);

        var documentIds = new List<string>();

        foreach (var vote in votes)
        {
            documentIds.Add(vote.DocumentId);
        }

        MerkleTree merkleTree = new MerkleTree(documentIds);
        await _electionRepository.UpdateElectionRootHash(electionId, merkleTree.RootHash);
    }
}