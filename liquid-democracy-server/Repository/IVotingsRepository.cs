namespace Repository;

public interface IVotingsRepository
{
    public Task<Votings?> CreateAsync(int userId, int elecitonId, int candidateId);
    public Task<IEnumerable<Votings?>> ReadAllAsync();

}