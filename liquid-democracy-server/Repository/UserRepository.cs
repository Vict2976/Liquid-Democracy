namespace Repository;

using Microsoft.EntityFrameworkCore;
using Security;
using Core;

public class UserRepository : IUserRepository
{
    ILiquidDemocracyContext _context;
    private IHasher _hasher;

    public UserRepository(ILiquidDemocracyContext context, IHasher hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task<User> Create(string providerId, DateTime sessionExpires)
    {
        var existingUser = _context.Users.Any(u => u.ProivderId.Equals(providerId));

        if (existingUser){

            return null;
        }

        var user = new User
        {
            ProivderId = providerId,
            SesseionExpires = sessionExpires,
            Email = null,
        };

        _context.Users.Add(user);

        _context.SaveChanges();

        return user;
        
    }

    public async Task<IEnumerable<User>?> ReadAllAsync(){
        var users = await _context.Users
            .ToListAsync();
        return users;
    }

    public async Task<User> GetByProiverId(string providerId){
        var user = _context.Users.Where(u => u.ProivderId == providerId).Select(u => u).First();

        if (user == null)
        {
            return null;
        }

        return user; 
    }

    public async Task<User> UpdateSessionTime(string providerId, DateTime updatedExpireTime){
        var user = _context.Users.Where(u => u.ProivderId == providerId).Select(u => u).First();
        user.SesseionExpires = updatedExpireTime;
        _context.Users.Update(user);
        return user;
    }
}