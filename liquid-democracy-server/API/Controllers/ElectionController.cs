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
    [ProducesResponseType(400)]
    public async Task<ActionResult<Election?>> Post([FromBody] CreateElectionDTO createElectionDTO)
    {
        var response = await _repository.CreateAsync(createElectionDTO.Name, createElectionDTO.Description, createElectionDTO.CreatedDate, createElectionDTO.Candidates);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Election>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Election?>>> Get()
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
    [ProducesResponseType(typeof(Election), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Election?>> GetElectionByID(int electionId)
    {
        var response = await _repository.GetElectionByIDAsync(electionId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpPut("{electionId}")]
    [ProducesResponseType(typeof(Election), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Response>> CloseElection(int electionId)
    {
        var response = await _repository.CloseElection(electionId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }
}