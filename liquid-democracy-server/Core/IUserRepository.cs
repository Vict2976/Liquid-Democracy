namespace Core;

public interface IUserRepository
{
    public Task<UserDTO?> CreateAsync(string name);
    public Task<UserDTO?> ReadAsync(int Id);

}