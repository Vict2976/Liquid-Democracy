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
    [ProducesResponseType(400)]
    public async Task<ActionResult<Candidate>> Post(string name, int electionId)
    {
        var response = await _repository.CreateAsync(name, electionId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Candidate>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates()
    {
        var response = await _repository.ReadAllAsync();

        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList();
    }

    [AllowAnonymous]
    [HttpGet("{electionId}")]
    [ProducesResponseType(typeof(IEnumerable<Candidate>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Candidate?>>> GetCandidatesForElection(int electionId)
    {
        var response = await _repository.ReadAllByElectionId(electionId);
        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList();
    }

/*     [AllowAnonymous]
    [HttpGet]
    [Route("countVoters/{electionId}")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<ActionResult<int>> AmountOfCandidateVotesForEleetion(int electionId)
    {
        var candidates = await _repository.ReadAllByElectionId(electionId);

        var amountOfRecievedVotes = 0;
        foreach (var candidate in candidates)
        {
            amountOfRecievedVotes += candidate.RecievedVotes;
        }
        return amountOfRecievedVotes;
    } */

/*     [AllowAnonymous]
    [HttpGet]
    [Route("findWinner/{electionId}")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<Candidate> FindWinner(int electionId)
    {
        var candidates = await _repository.ReadAllByElectionId(electionId);
        var candidateWinner = new Candidate();
        foreach (var candidate in candidates)
        {
            if (candidate.RecievedVotes > candidateWinner.RecievedVotes)
            {
                candidateWinner = candidate;
            }
        }
        return candidateWinner;
    } */
}