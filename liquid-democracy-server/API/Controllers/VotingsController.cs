namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VotingsController : ControllerBase
{
    readonly IVotingsRepository _repository;

    public VotingsController(IVotingsRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(Votings), 201)]
    public async Task<ActionResult<Votings?>> Post(int userId, int elecitonId, int candidateId)
    {
        var response = await _repository.CreateAsync(userId, elecitonId, candidateId);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Votings?>> Get(){
        var votings = await _repository.ReadAllAsync();
        return votings;
    }
}