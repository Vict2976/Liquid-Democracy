using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VoteRepository : IVoteRepository
{
    ILiquidDemocracyContext _context;

    public VoteRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Vote?> CreateAsync(int userId, int elecitonId, int candidateId){
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
}