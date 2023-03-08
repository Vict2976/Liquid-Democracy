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
    public async Task<ActionResult<VoteUsedOn?>> CreateVoteUsedOnForCandidate([FromBody] VoteUsedOnCandidateDTO voteUsedOnCandidateDTO)
    {
        var response = await _repository.CreateVoteUsedOnForCandidate(voteUsedOnCandidateDTO.voteId, voteUsedOnCandidateDTO.candidateId);
        return response;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/ForDelegate")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<ActionResult<VoteUsedOn?>> CreateVoteUsedOnForDelegate([FromBody] VoteUsedOnDelegateDTO voteUsedOnDelegateDTO)
    {
        var response = await _repository.CreateVoteUsedOnForDelegate(voteUsedOnDelegateDTO.voteId, voteUsedOnDelegateDTO.delegateId, voteUsedOnDelegateDTO.electionId);
        return response;
    }

}