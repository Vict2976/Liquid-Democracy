using DataModels;

namespace Repository;

public interface ITallyRepository
{
    public Task<string> FindWinner();
}