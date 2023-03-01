using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VoteRepository : IVoteRepository
{
    ILiquidDemocracyContext _context;

    public VoteRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Vote?> CreateAsync(int userId, int elecitonId){
        var vote = new Vote
            {
                BelongsToId = userId,
                ElectionId = elecitonId,
            };
        
        _context.Votes.Add(vote);

        await _context.SaveChangesAsync();

        return vote;    
    }

    public async Task<IEnumerable<Vote>?> ReadAllAsync(){
        var votes = await _context.Votes
            .ToListAsync();
        return votes;
    }

    public async Task<Vote?> checkForExistingVote(int belongsToId, int elecitonId)
    {
        try{ 
             var vote = await _context.Votes.Where(c=> c.BelongsToId==belongsToId && c.ElectionId == elecitonId).FirstAsync();
             return vote;
        }catch{
            return null;
        }
    }
}