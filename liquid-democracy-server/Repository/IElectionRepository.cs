using DataModels;

namespace Repository;

public interface IElectionRepository
{
    public Task<Election?> CreateAsync(string name, string description, DateTime createdDate, ICollection<string> candidates);
    public Task<IEnumerable<Election>?> ReadAllAsync();
    public Task<Election?> GetElectionByIDAsync(int electionId);
    public Task<int> GetLatestBallotId();
    public Task<Response<Election>> CloseElection(int electionId);
    public  Task<Response<Election>> UpdateElectionRootHash(int electionId, string rootHash);

}