using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository;

public class ElectionRepository : IElectionRepository
{
    ILiquidDemocracyContext _context;
    ICandidateRepository _candidateRepository;
    IBlockRepository _blockRepository;

    private readonly IConfiguration _config;

    public ElectionRepository(ILiquidDemocracyContext context, ICandidateRepository candidateRepository, IBlockRepository blockRepository, IConfiguration config)
    {
        _context = context;
        _candidateRepository = candidateRepository;
        _blockRepository = blockRepository;
        _config = config;
    }

    public async Task<Election?> CreateAsync(string name, string description, DateTime createdDate, ICollection<string> candidates)
    {

        var genBlock = new List<string> { _config["BlockChain:GenesisBlock"] };
        var gRT = new MerkleTree(genBlock).RootHash;

        var election = new Election
        {
            Name = name,
            Description = description,
            CreatedDate = createdDate,
            IsEnded = false,
            RootHash = gRT
        };

        _context.Elections.Add(election);

        await _context.SaveChangesAsync();

        foreach (var candidate in candidates)
        {
            await _candidateRepository.CreateAsync(candidate, election.ElectionId);
        }

        await _blockRepository.CreateAsync(election.ElectionId, gRT);

        return election;
    }

    public async Task<IEnumerable<Election>?> ReadAllAsync()
    {
        var elections = await _context.Elections
            .ToListAsync();
        return elections;
    }



    public async Task<Election?> GetElectionByIDAsync(int electionId)
    {
        var election = await _context.Elections.Where(c => c.ElectionId == electionId).Select(c => c).FirstAsync();

        return election;
    }

    public async Task<Response<Election>> CloseElection(int electionId)
    {
        var election = await GetElectionByIDAsync(electionId);
        election.IsEnded = true;
        _context.Elections.Update(election);
        await _context.SaveChangesAsync();

        return new Response<Election>
        {
            HTTPResponse = HTTPResponse.Success,
            Model = election
        };
    }

    public async Task<Response<Election>> UpdateElectionRootHash(int electionId, string rootHash)
    {
        var election = await GetElectionByIDAsync(electionId);
        election.RootHash = rootHash;
        _context.Elections.Update(election);
        await _context.SaveChangesAsync();

        return new Response<Election>
        {
            HTTPResponse = HTTPResponse.Success,
            Model = election
        };
    }
}