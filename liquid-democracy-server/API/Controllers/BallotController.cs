namespace Server.Controllers;

using System.Security.Cryptography;
using Repository;
using Security;


[ApiController]
[Route("[controller]")]
public class BallotController : ControllerBase
{
    readonly IBallotRepository _repository;

    public BallotController(IBallotRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/CreateBallot/{candidateId}/{uuid}/{documentId}")]
    [ProducesResponseType(typeof(RedirectResult), 201)]
    [ProducesResponseType(400)]
    public async Task<RedirectResult> Post(string candidateId, string uuid, string documentId)
    {
        try
        {
            var response = await _repository.CreateAsync(candidateId, uuid, documentId);
            if (response == null)
            {
                return new RedirectResult(url: "http://localhost:3000/Error", permanent: true, preserveMethod: true);
            }
            return new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
        }
        catch (Exception e)
        {
            return new RedirectResult(url: "http://localhost:3000/Error", permanent: true, preserveMethod: true);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/GetBallot/{ballotId}")]
    [ProducesResponseType(typeof(Ballot), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Ballot?>> GetBallot(int ballotId)
    {
        var response = await _repository.GetBallotByIdAsync(ballotId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/DecryptBallot/{ballotId}")]
    [ProducesResponseType(typeof(Ballot), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Ballot?>> DecryptBallot(int ballotId)
    {
        var response = await _repository.DecryptBallotById(ballotId);
        if (response == null)
        {
            return BadRequest();
        }
        return response;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/GetValidBallots/")]
    [ProducesResponseType(typeof(IEnumerable<Ballot>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<Ballot>>> RemoveAllInvalidBallots()
    {
        var response = await _repository.FindAndDeleteAllOldVotes();
        if (response == null)
        {
            return BadRequest();
        }
        return response.ToList();
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/TestHashChain/")]
    [ProducesResponseType(typeof(IEnumerable<Ballot>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<bool>> VerifyHash(int electionid)
    {
        var response = await _repository.VerifyHashChain(electionid);
        return response;
    }

}
