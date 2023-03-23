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
    public async Task<IEnumerable<User>> Get(){
        var users = await _repository.ReadAllAsync();
        return users;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<User> CreateUser([FromBody] UserDTO userDto){
        var user = await _repository.Create(userDto.ProiverId, userDto.SessionExpires);
        return user;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/GetUser/{providerId}")]  
    public async Task<User?> GetUser(string providerId){
        try {
            var user = await _repository.GetByProiverId(providerId);
            return user;
        }catch (Exception e){
            return null;
        }
    }

    [AllowAnonymous]
    [HttpPut]
    [Route("/UpdateSession")]  
    public async Task<User?> UpdateSession([FromBody] UserDTO userDto){
        try {
            var user = await _repository.UpdateSessionTime(userDto.ProiverId, userDto.SessionExpires);
            return user;
        }catch (Exception e){
            return null;
        }
    }
}