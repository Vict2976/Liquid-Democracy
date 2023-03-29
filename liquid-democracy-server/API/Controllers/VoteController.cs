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
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Vote?>> Post([FromBody] VoteDTO voteDTO)
    {
        var checkForVote = _repository.checkForExistingVote(voteDTO.userId, voteDTO.electionId);

        if (checkForVote.Result != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var response = await _repository.CreateAsync(voteDTO.userId, voteDTO.electionId, voteDTO.documentId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(Vote), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Vote?>>> Get()
    {
        var response = await _repository.ReadAllAsync();
        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList();
    }

    [AllowAnonymous]
    [Route("/countVoters/{electionId}")]
    [HttpGet]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<int>> GetAmountOfVoters(int electionId)
    {
        var response = await _repository.ReadFromElectionId(electionId);

        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList().Count();
    }
}