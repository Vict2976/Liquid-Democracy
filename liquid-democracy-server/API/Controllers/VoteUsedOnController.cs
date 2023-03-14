namespace Server.Controllers;

using Repository;

[ApiController]
[Route("[controller]")]
public class VoteUsedOnController : ControllerBase
{
    readonly IVoteUsedOnRepository _repository;
    readonly IVoteRepository _voteRepository;


    public VoteUsedOnController(IVoteUsedOnRepository repository, IVoteRepository voteRepository)
    {
        _repository = repository;
        _voteRepository = voteRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/ForCandidate/{userId}/{electionId}/{candidateId}")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<RedirectResult> CreateVoteUsedOnForCandidate(int userId, int electionId, int candidateId)
    {
        var checkForVote = _voteRepository.checkForExistingVote(userId, electionId);

        if (checkForVote.Result != null)
        {
            return null;
        }

        var vote = await _voteRepository.CreateAsync(userId, electionId);

        await _repository.CreateVoteUsedOnForCandidate(vote.VoteId, candidateId);

        return  new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/ForDelegate/{userId}/{electionId}/{delegateId}")]
    [ProducesResponseType(typeof(VoteUsedOn), 201)]
    public async Task<RedirectResult> CreateVoteUsedOnForDelegate(int userId, int electionId, int delegateId)
    {
        var checkForVote = _voteRepository.checkForExistingVote(userId, electionId);

        if (checkForVote.Result != null)
        {
            return null;
        }

        var vote = await _voteRepository.CreateAsync(userId, electionId);


        var response = await _repository.CreateVoteUsedOnForDelegate(vote.VoteId, delegateId, electionId);


        return  new RedirectResult(url: "http://localhost:3000/Register", permanent: true, preserveMethod: true);
    }
}