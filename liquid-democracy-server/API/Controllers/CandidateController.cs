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
    public async Task<IEnumerable<Candidate?>> Get(){
        var candidates = await _repository.ReadAllAsync();
        return candidates;
    }
}