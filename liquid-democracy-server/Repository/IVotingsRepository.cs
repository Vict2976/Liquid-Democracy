namespace Repository;

public interface IVoteRepository
{
    public Task<Vote?> CreateAsync(int userId, int elecitonId, string documentId);
    public Task<IEnumerable<Vote?>> ReadAllAsync();
    public Task<IEnumerable<Vote?>> ReadFromElectionId(int electionId);
    public Task<Vote?> checkForExistingVote(int belongsToId, int elecitonId);
}