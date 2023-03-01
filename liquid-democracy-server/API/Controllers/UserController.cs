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

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Route("/register")]
    public void Register([FromBody] RegisterDTO registerDTO)
    {
        _hasher.Hash(registerDTO.Password, out string hashedPassword);
        var response = _repository.Create(registerDTO.Username, registerDTO.Email, hashedPassword);
        response.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<User>> Get(){
        var users = await _repository.ReadAllAsync();
        return users;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("/login")]
    public IActionResult Login([FromBody]LoginDTO loginDTO)
    {
        var response = _repository.GetByUsername(loginDTO.Username);

        if (response.HTTPResponse == HTTPResponse.NotFound)
        {
            return Unauthorized("Invalid username");
        }

        var validPassword = _hasher.VerifyHash(loginDTO.Password, response.Model!.Password!);

        if (!validPassword)
        {
            return Unauthorized("Invalid password");
        }

        return response.ToActionResult();
    }

    [HttpGet("{electionId}")]
    [AllowAnonymous]
    public Task<IEnumerable<User>> GetAllDelegatesByElection(int electionId)
    {
        var response = _repository.GetAllDelegetasByElection(electionId);
        return response;
    }
}