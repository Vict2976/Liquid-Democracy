namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class ElectionController : ControllerBase
{
    readonly IElectionRepository _repository;

    public ElectionController(IElectionRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("{name}")]
    [ProducesResponseType(typeof(Election), 201)]
    public async Task<ActionResult<Election?>> Post(string name, int userId)
    {
        var response = await _repository.CreateAsync(name, userId);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Election?>> Get(){
        var elections = await _repository.ReadAllAsync();
        return elections;
    }

    [AllowAnonymous]
    [HttpGet ("{electionId}")]
    public async Task<Election?> GetElectionByID(int electionId){
        var election = await _repository.GetElectionByIDAsync(electionId);
        return election;
    }

}