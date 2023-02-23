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
    public async Task<ActionResult<Vote?>> Post(int userId, int elecitonId, int candidateId)
    {
        var response = await _repository.CreateAsync(userId, elecitonId, candidateId);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Vote?>> Get(){
        var votes = await _repository.ReadAllAsync();
        return votes;
    }
}