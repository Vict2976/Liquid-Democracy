using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VotingsRepository : IVotingsRepository
{
    ILiquidDemocracyContext _context;

    public VotingsRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Votings?> CreateAsync(int userId, int elecitonId, int candidateId){
        var voting = new Votings
            {
                UserId = userId,
                AmountOfVotes = 1,
                ElectionId = elecitonId,
                CandidateId = candidateId
            };
        
        _context.Votings.Add(voting);

        await _context.SaveChangesAsync();

        return voting;    
    }

    public async Task<IEnumerable<Votings>?> ReadAllAsync(){
        var votings = await _context.Votings
            .ToListAsync();
        return votings;
    }
}