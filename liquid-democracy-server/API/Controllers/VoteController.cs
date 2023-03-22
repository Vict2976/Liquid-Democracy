namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    readonly IVoteRepository _repository;

    public VoteController(IVoteRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(Vote), 201)]
    public async Task<ActionResult<Vote?>> Post([FromBody] VoteDTO voteDTO)
    {
        var checkForVote = _repository.checkForExistingVote(voteDTO.userId, voteDTO.electionId);

        if (checkForVote.Result != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var response = await _repository.CreateAsync(voteDTO.userId, voteDTO.electionId, voteDTO.documentId);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Vote?>> Get(){
        var votes = await _repository.ReadAllAsync();
        return votes;
    }

    [AllowAnonymous]
    [Route("/countVoters/{electionId}")]
    [HttpGet]
    public async Task<int> GetAmountOfVoters(int electionId){
        var votes = await _repository.ReadFromElectionId(electionId);
        return votes.Count();
    }
}