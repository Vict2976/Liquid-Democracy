using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TallyRepository : ITallyRepository
{

    ILiquidDemocracyContext _context;

    public TallyRepository(ILiquidDemocracyContext context){
        _context = context;
    }

    public async Task<string> FindWinner()
    {
        var allEntrys = await _context.Tallies.ToListAsync();
        
        var mostCommon = allEntrys
            .GroupBy(i => i.candidateId)
            .OrderBy(g => g.Count())
            .Select(g => g.Key)
            .LastOrDefault();

        var winnerId = mostCommon;

        var winner = await _context.Candidates.Where(c => c.CandidateId == winnerId).FirstAsync();

        return winner.Name;
    }
}