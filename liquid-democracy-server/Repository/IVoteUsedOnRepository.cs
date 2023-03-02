namespace Repository;

public interface IVoteUsedOnRepository
{
    public Task<VoteUsedOn?> CreateVoteUsedOnForCandidate(int voteId, int? candidateId);

    public Task<VoteUsedOn?> CreateVoteUsedOnForDelegate(int voteId, int? delegateId, int elecitonId);

}