namespace Repository;

using Core;

public interface IUserRepository
{ 
    Response<User> Create(string username, string email, string password);
    Task<IEnumerable<User>> ReadAllAsync();
    Response<User> GetByUsername(string username);

}