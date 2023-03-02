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
    [Route("/ForCandidate")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<ActionResult<VoteUsedOn?>> CreateVoteUsedOnForCandidate(int voteId, int? candidateId)
    {
        var response = await _repository.CreateVoteUsedOnForCandidate(voteId, candidateId);
        return response;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/ForDelegate")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<ActionResult<VoteUsedOn?>> CreateVoteUsedOnForDelegate(int voteId, int? delegateId, int electionId)
    {
        var response = await _repository.CreateVoteUsedOnForDelegate(voteId, delegateId, electionId);
        return response;
    }

}