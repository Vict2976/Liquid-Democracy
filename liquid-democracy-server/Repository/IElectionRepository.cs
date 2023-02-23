namespace Repository;

public interface IElectionRepository
{
    public Task<Election?> CreateAsync(string name, string description, DateTime createdDate, List<Candidate> candidates);
    public Task<IEnumerable<Election>?> ReadAllAsync();
    public Task<Election?> GetElectionByIDAsync(int electionId);

}