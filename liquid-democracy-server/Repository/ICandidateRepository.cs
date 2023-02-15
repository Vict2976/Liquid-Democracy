namespace Repository;

public interface ICandidateRepository
{
    public Task<Candidate?> CreateAsync(string name, int electionId);
    public Task<IEnumerable<Candidate>?> ReadAllAsync();

}