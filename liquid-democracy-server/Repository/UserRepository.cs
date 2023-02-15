namespace Repository;

using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    ILiquidDemocracyContext _context;

    public UserRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateAsync(string name){
        var user = new User
            {
                Name = name
            };
        
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }


    public async Task<IEnumerable<User>?> ReadAllAsync(){
        var users = await _context.Users
            .ToListAsync();
        return users;
    }
}