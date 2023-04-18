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
    readonly IBlockRepository _blockRepository;

    private readonly IConfiguration _config;

    public VoteUsedOnController(
        IVoteUsedOnRepository repository,
        IVoteRepository voteRepository,
        IElectionRepository electionRepository,
        IUserRepository userRepository,
        IBlockRepository blockRepository,
        IConfiguration config)
    {
        _repository = repository;
        _voteRepository = voteRepository;
        _electionRepository = electionRepository;
        _userRepository = userRepository;
        _blockRepository = blockRepository;
        _config = config;
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

        await _repository.CreateVoteUsedOnForCandidate(vote.VoteId, candidateId);

        var allVotes = await _voteRepository.ReadFromElectionId(electionId);
        var amountOfVotes = allVotes.Count();

        if (amountOfVotes % 4 == 0){
            await AddNewBlock(electionId, allVotes);
        }

        return new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/VerifyRootHash/{electionId}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<bool> VeirfyElectionByRootHash(int electionId)
    {
        var canBeVerified = await VerifyElectionHashChain(electionId);
        return canBeVerified;
    }

    private async Task<bool> VerifyElectionHashChain(int electionId)
    {
        var election = await _electionRepository.GetElectionByIDAsync(electionId);
        var blocks = await _blockRepository.GetAllBlocksForElection(electionId);

        var RootHashForElection = election.RootHash;

        var genBlock = new List<string>{_config["BlockChain:GenesisBlock"]};
        var checkChain = new HashChain(genBlock);

        foreach(var block in blocks){
            var dataset = new List<string>{checkChain.BlockHash, block.BlockHash};
            checkChain = new HashChain(dataset);
        }

        if (RootHashForElection == checkChain.BlockHash)
        {
            return true;
        }
        else
        {
            return false;
        }
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

        var genBlock = new List<string>{_config["BlockChain:GenesisBlock"]};
        var checkTree = new MerkleTree(genBlock);

        foreach(var dataElement in documentIds){
            var dataset = new List<string>{checkTree.RootHash, dataElement};
            checkTree = new MerkleTree(dataset);
        }

        var currentRootHash = election.RootHash;

        if (RootHashForElection == checkTree.RootHash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private async Task buildNewRootHash(int electionId, string documentId)
    {
        var election = await _electionRepository.GetElectionByIDAsync(electionId);

        var oldRT = election.RootHash;
        var dataSet = new List<string>{oldRT, documentId};
        MerkleTree merkleTree = new MerkleTree(dataSet);
        await _electionRepository.UpdateElectionRootHash(electionId, merkleTree.RootHash);
    }

    private async Task AddNewBlock(int electionId, IEnumerable<Vote?>? allVotes){
        //Right now all votes are hashed together an added. We want only the most recent 4 votes. 

        var datasetForBlock = new List<string>();
        foreach(var vote in allVotes){
            datasetForBlock.Add(vote.DocumentId);
        }

        var merkleTree = new MerkleTree(datasetForBlock);
        await _blockRepository.CreateAsync(electionId, merkleTree.RootHash);

        //Add new hashChain to election
        var election = await _electionRepository.GetElectionByIDAsync(electionId);
        var currentRootHash = election.RootHash;
        var dataset = new List<string>{currentRootHash, merkleTree.RootHash};
        var chainLink = new HashChain(dataset);
        election.RootHash = chainLink.BlockHash;
    }
}

//TO:DO::
// VerifyRootHash skal nu linke alle blokke sammen og checke op med current hash
// BuildNewRootHash skal kun køres hver 4. gang vi tilføjer en ny stemme (Datasæte = 4 nyeste stemme inkl. den vi lige har modtaget)
// Hver gang den køres, skal vi lave en ny Block ud fra Current hash og nye merkle tree root hash 