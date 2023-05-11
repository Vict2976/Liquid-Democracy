namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class TallyController : ControllerBase
{
    readonly ITallyRepository _repository;

    public TallyController(ITallyRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> Get()
    {
        var response = await _repository.FindWinner();
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }
}