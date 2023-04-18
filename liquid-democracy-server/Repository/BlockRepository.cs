using System.Data.Entity;

namespace Repository;

public class BlockRepository : IBlockRepository
{

    ILiquidDemocracyContext _context;

    public BlockRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }
    public async Task<Block?> CreateAsync(int electionId, string blockHash)
    {
        var block = new Block
            {
                ElectionId = electionId,
                BlockHash = blockHash
            };
        
        _context.Blocks.Add(block);

        await _context.SaveChangesAsync();

        return block;   
    }

    public async Task<IEnumerable<Block>?> GetAllBlocksForElection(int electionId){
        var blocks = await _context.Blocks.Where(b => b.ElectionId == electionId).ToListAsync();
        return blocks;
    }

}

