namespace Repository;

public interface IVoteUsedOnRepository
{
    public Task<VoteUsedOn?> CreateVoteUsedOn(int voteId, int? candidateId, int? delegateId);

}