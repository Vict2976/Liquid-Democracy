using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ElectionRepository : IElectionRepository
{
    ILiquidDemocracyContext _context;

    public ElectionRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Election?> CreateAsync(string name, string description, DateTime createdDate, List<Candidate> candidates){

        var election = new Election
            {
                Name = name,
                Description = description,
                CreatedDate = createdDate,
                Candidates = candidates,
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



    public async Task<Election?> GetElectionByIDAsync(int electionId){
        var election = await _context.Elections.Where(c => c.ElectionId == electionId).Select(c=> c).FirstAsync();

        return election;
    }
}