namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class CandidateController : ControllerBase
{
    readonly ICandidateRepository _repository;

    public CandidateController(ICandidateRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("{name}")]
    [ProducesResponseType(typeof(Candidate), 201)]
    public async Task<ActionResult<Candidate>> Post(string name, int electionId)
    {
        var response = await _repository.CreateAsync(name, electionId);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Candidate?>> GetAllCandidates(){
        var candidates = await _repository.ReadAllAsync();
        return candidates;
    }

    [AllowAnonymous]
    [HttpGet("{electionId}")]
    public async Task<IEnumerable<Candidate?>> GetCandidatesForElection(int electionId){
        var candidates = await _repository.ReadAllByElectionId(electionId);
        return candidates;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("countVoters/{electionId}")]
    public async Task<int> AmountOfCandidateVotesForEleetion(int electionId){
        var candidates = await _repository.ReadAllByElectionId(electionId);
        var amountOfRecievedVotes = 0;
        foreach(var candidate in candidates){
            amountOfRecievedVotes += candidate.RecievedVotes;
        }
        return amountOfRecievedVotes;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("findWinner/{electionId}")]
    public async Task<Candidate> FindWinner(int electionId){
        var candidates = await _repository.ReadAllByElectionId(electionId);
        var candidateWinner = new Candidate();
        foreach(var candidate in candidates){
            if(candidate.RecievedVotes > candidateWinner.RecievedVotes){
                candidateWinner = candidate;
            }
        }
        return candidateWinner;
    }

}