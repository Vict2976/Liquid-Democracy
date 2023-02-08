namespace Entities;

using System.ComponentModel.DataAnnotations;
using Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Core;

public class UserRepository : IUserRepository
{
    ILiquidDemocracyContext _context;

    public UserRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<UserDTO?> CreateAsync(string name){
        var Entity = new User
            {
                Name = name
            };
        
        _context.Users.Add(Entity);

        await _context.SaveChangesAsync();

        return new UserDTO{Name = name};
    }


    public async Task<UserDTO?> ReadAsync(int Id){
        var user = await _context.Users
            .Where(u => u.Id == Id)
            .FirstAsync();

            return new UserDTO{Name = user.Name};
    }
}