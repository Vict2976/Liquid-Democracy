namespace Repository;

public interface IBallotRepository
{
    public Task<Ballot?> CreateAsync(string candidateId, string uuid, string documentId);
    public Task<Ballot?> GetBallotByIdAsync(int ballotId);
    public Task<DocumentInformation> GetDocumentInfo(string documentId, string token);
    public Task<IEnumerable<Ballot>> FindAndDeleteAllOldVotes();
    public Task<Ballot> DecryptBallotById(int ballotId);

}