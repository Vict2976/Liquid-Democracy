using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ElectionRepository : IElectionRepository
{
    ILiquidDemocracyContext _context;
    ICandidateRepository _candidateRepository;

    public ElectionRepository(ILiquidDemocracyContext context, ICandidateRepository candidateRepository)
    {
        _context = context;
        _candidateRepository = candidateRepository;
    }

    public async Task<Election?> CreateAsync(string name, string description, DateTime createdDate, ICollection<string> candidates){

        var election = new Election
            {
                Name = name,
                Description = description,
                CreatedDate = createdDate,
            };
        _context.Elections.Add(election);

        await _context.SaveChangesAsync();

        foreach (var candidate in candidates){
            await _candidateRepository.CreateAsync(candidate, election.ElectionId);
        }
        


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