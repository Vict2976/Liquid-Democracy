using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ElectionRepository : IElectionRepository
{
    ILiquidDemocracyContext _context;

    public ElectionRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Election?> CreateAsync(string name, int userId){
        var election = new Election
            {
                Name = name,
                UserId = userId
            };
        
        _context.Elections.Add(election);

        await _context.SaveChangesAsync();

        return election;

    }

    public async Task<IEnumerable<Election>?> ReadAllAsync(){
        var elections = await _context.Elections
            .ToListAsync();
        return elections;
    }
}