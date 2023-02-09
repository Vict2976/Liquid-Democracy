namespace Server.Controllers;

using Core;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("{name}")]
    [ProducesResponseType(typeof(UserDTO), 201)]
    public async Task<ActionResult<UserDTO>> Post(string name)
    {
        var response = await _repository.CreateAsync(name);
        return response;
    }

    [AllowAnonymous]
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDTO>> Get(int Id){
        var response = await _repository.ReadAsync(Id);
        return response;
    }
}