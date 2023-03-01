namespace Repository;

public interface IVoteRepository
{
    public Task<Vote?> CreateAsync(int userId, int elecitonId, int candidateId);
    public Task<IEnumerable<Vote?>> ReadAllAsync();
    public Task<Vote?> checkForExistingVote(int belongsToId, int elecitonId);

}