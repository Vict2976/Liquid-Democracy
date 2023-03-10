namespace Server.Controllers;

using Core;
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
    [HttpPost]
    [ProducesResponseType(typeof(Election), 201)]
    public async Task<ActionResult<Election?>> Post([FromBody] CreateElectionDTO createElectionDTO)
    {
        var response = await _repository.CreateAsync(createElectionDTO.Name, createElectionDTO.Description, createElectionDTO.CreatedDate, createElectionDTO.Candidates);
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

    [AllowAnonymous]
    [HttpPut ("{electionId}")]
    public async Task<Response> CloseElection(int electionId){
        var response = await _repository.CloseElection(electionId);
        return response;
    }

}