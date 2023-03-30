namespace Repository;

public interface IVoteUsedOnRepository
{
    public Task<VoteUsedOn?> CreateVoteUsedOnForCandidate(int voteId, int? candidateId);

}