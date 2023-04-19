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

        var originalDataSet = new List<string>{name, electionId.ToString()};
        var rootHash = new MerkleTree(originalDataSet).RootHash;

        var candidate = new Candidate
            {
                Name = name,
                ElectionId = electionId,
                RootHash = rootHash
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

    public async Task<Candidate>? GetById(int candidateId)
    {
        var candidate = await _context.Candidates.Where(c => c.CandidateId == candidateId).FirstAsync();
        return candidate;

    }
}