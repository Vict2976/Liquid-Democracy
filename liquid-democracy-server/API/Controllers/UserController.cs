namespace Server.Controllers;

using Repository;
using Core;
using Security;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly IUserRepository _repository;
    private IHasher _hasher;

    public UserController(IUserRepository repository, IHasher hasher)
    {
        _repository = repository;
        _hasher = hasher;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var response = await _repository.ReadAllAsync();
        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList();
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(User), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserDTO userDto)
    {
        var response = await _repository.Create(userDto.ProiverId, userDto.SessionExpires);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/GetUser/{providerId}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<User>> GetUser(string providerId)
    {
        try
        {
            var response = await _repository.GetByProiverId(providerId);
            if (response == null)
            {
                return BadRequest();
            }
            return response;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPut]
    [Route("/UpdateSession")]
    public async Task<ActionResult<User?>> UpdateSession([FromBody] UserDTO userDto)
    {
        try
        {
            var response = await _repository.UpdateSessionTime(userDto.ProiverId, userDto.SessionExpires);
            if (response == null)
            {
                return BadRequest();
            }
            return response;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}