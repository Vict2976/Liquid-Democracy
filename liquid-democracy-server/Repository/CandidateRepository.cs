namespace Repository;

using Microsoft.EntityFrameworkCore;

public class CandidateRepository : ICandidateRepository
{
    ILiquidDemocracyContext _context;

    public CandidateRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Candidate?> CreateAsync(string name, int electionId){
        var candidate = new Candidate
            {
                Name = name,
                ElectionId = electionId,
                RecievedVotes = 0
            };
        
        _context.Candidates.Add(candidate);

        await _context.SaveChangesAsync();

        return candidate;   
    }

    public async Task<IEnumerable<Candidate>?> ReadAllAsync(){
        var candidates = await _context.Candidates
            .ToListAsync();
        return candidates;
    }

    public async Task<IEnumerable<Candidate>?> ReadAllByElectionId(int electionId)
    {
        var candidates = await _context.Candidates.Where(c => c.ElectionId == electionId).ToListAsync();
        return candidates;

    }
}