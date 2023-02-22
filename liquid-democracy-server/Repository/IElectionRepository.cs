namespace Repository;

public interface IElectionRepository
{
    public Task<Election?> CreateAsync(string name, int userId);
    public Task<IEnumerable<Election>?> ReadAllAsync();

}