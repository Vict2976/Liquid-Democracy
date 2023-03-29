namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VoteUsedOnController : ControllerBase
{
    readonly IVoteUsedOnRepository _repository;
    readonly IVoteRepository _voteRepository;
    readonly IElectionRepository _electionRepository;


    public VoteUsedOnController(IVoteUsedOnRepository repository, IVoteRepository voteRepository, IElectionRepository electionRepository)
    {
        _repository = repository;
        _voteRepository = voteRepository;
        _electionRepository = electionRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/ForCandidate/{userId}/{electionId}/{candidateId}/{documentId}")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<RedirectResult> CreateVoteUsedOnForCandidate(int userId, int electionId, int candidateId, string documentId)
    {
        var checkForVote = _voteRepository.checkForExistingVote(userId, electionId);

        if (checkForVote.Result != null)
        {
            return null;
        }

        Console.WriteLine("TESTHALLO");
        if (!await verifyRootHash(electionId)){
            Console.WriteLine("CANNOT VERIFY ROOT HASH");
            return null;
        }

        var vote = await _voteRepository.CreateAsync(userId, electionId, documentId);
        await buildNewRootHash(electionId);

        await _repository.CreateVoteUsedOnForCandidate(vote.VoteId, candidateId);

        return  new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/ForDelegate/{userId}/{electionId}/{delegateId}/{documentId}")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<RedirectResult> CreateVoteUsedOnForDelegate(int userId, int electionId, int delegateId, string documentId)
    {
        var checkForVote = _voteRepository.checkForExistingVote(userId, electionId);

        if (checkForVote.Result != null)
        {
            return null;
        }

        var vote = await _voteRepository.CreateAsync(userId, electionId, documentId);


        var response = await _repository.CreateVoteUsedOnForDelegate(vote.VoteId, delegateId, electionId);


        return  new RedirectResult(url: "http://localhost:3000/Register", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/VerifyRootHash/{electionId}")]
    public async Task<bool> VeirfyElectionByRootHash(int electionId){
        var canBeVerified = await verifyRootHash(electionId);
        return canBeVerified;
    }


    private async Task<bool> verifyRootHash(int electionId){
        var votes = await _voteRepository.ReadFromElectionId(electionId);
        var election = await _electionRepository.GetElectionByIDAsync(electionId);

        if (election == null){
            return false;
        }

        if (votes.Count() == 0){
            return true;
        }

        var RootHashForElection = election.RootHash;
        var documentIds = new List<string>();

        foreach(var vote in votes){            
            documentIds.Add(vote.DocumentId);
        }

        MerkleTree merkleTree = new MerkleTree(documentIds);
        if (RootHashForElection == merkleTree.RootHash){
            return true;
        }else{
            return false;
        }
    }

    private async Task buildNewRootHash(int electionId){
        var votes = await _voteRepository.ReadFromElectionId(electionId);

        var documentIds = new List<string>();

        foreach(var vote in votes){
            documentIds.Add(vote.DocumentId);
        }

        MerkleTree merkleTree = new MerkleTree(documentIds);
        await _electionRepository.UpdateElectionRootHash(electionId, merkleTree.RootHash);
    }
}