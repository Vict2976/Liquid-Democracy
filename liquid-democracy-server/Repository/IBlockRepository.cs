namespace Repository;

public interface IBlockRepository
{
    public Task<Block?> CreateAsync(int electionId, string blockHash);
    public Task<IEnumerable<Block>?> GetAllBlocksForElection(int electionId);

}