using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VoteRepository : IVoteRepository
{
    ILiquidDemocracyContext _context;

    public VoteRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Vote?> CreateAsync(int userId, int elecitonId, string documentId){
        var vote = new Vote
            {
                BelongsToId = userId,
                ElectionId = elecitonId,
                DocumentId = documentId
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

    public async Task<IEnumerable<Vote?>> ReadFromElectionId(int electionId){
        try{ 
            var votes = await _context.Votes.Where(v => v.ElectionId == electionId).ToListAsync();
             return votes;
        }catch{
            return null;
        }
    }
}