namespace Repository;

using Core;

public interface IUserRepository
{ 
    Task<User> Create(string providerId, DateTime sessionExpires);
    Task<IEnumerable<User>> ReadAllAsync();
    Task<User?> GetByProiverId(string providerId);

    Task<User> UpdateSessionTime(string providerId, DateTime updatedExpireTime);

}