namespace Server.Controllers;

using Repository;

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
    [ProducesResponseType(typeof(User), 201)]
    public async Task<ActionResult<User?>> Post(string name)
    {
        var response = await _repository.CreateAsync(name);
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<User>> Get(){
        var users = await _repository.ReadAllAsync();
        return users;
    }
}