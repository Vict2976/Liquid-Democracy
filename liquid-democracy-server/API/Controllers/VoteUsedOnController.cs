namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VoteUsedOnController : ControllerBase
{
    readonly IVoteUsedOnRepository _repository;

    public VoteUsedOnController(IVoteUsedOnRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<ActionResult<VoteUsedOn?>> Post(int voteId, int? candidateId, int? delegateId)
    {
        var response = await _repository.CreateVoteUsedOn(voteId, candidateId, delegateId);
        return response;
    }

}