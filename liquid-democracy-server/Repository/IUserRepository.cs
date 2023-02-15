namespace Repository;

public interface IUserRepository
{
    public Task<User?> CreateAsync(string name);
    public Task<IEnumerable<User>> ReadAllAsync();

}