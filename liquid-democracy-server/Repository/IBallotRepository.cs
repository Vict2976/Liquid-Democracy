namespace Repository;

public interface IBallotRepository
{
    public Task<Ballot?> CreateAsync(string candidateId, string providerId, string documentId);
    public Task<Ballot?> GetBallotByIdAsync(int ballotId);
    public  Task<DocumentInformation> GetDocumentInfo(string documentId);

}