namespace Server.Controllers;

using System.Text;
using Repository;
using Security;
using Core;


[ApiController]
[Route("[controller]")]
public class BallotController : ControllerBase
{
    readonly IBallotRepository _repository;

    public BallotController(IBallotRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/CreateBallot/{candidateId}/{providerId}/{documentId}")]
    [ProducesResponseType(typeof(RedirectResult), 201)]
    [ProducesResponseType(400)]
    public async Task<RedirectResult> Post(string candidateId, string providerId, string documentId)
    {
        var response = await _repository.CreateAsync(candidateId, providerId, documentId);
        if (response == null)
        {
            return new RedirectResult(url: "http://localhost:3000/Error", permanent: true, preserveMethod: true);
        }
        return new RedirectResult(url: "http://localhost:3000/", permanent: true, preserveMethod: true);
    }

    [AllowAnonymous]
    [HttpGet("{ballotId}")]
    [ProducesResponseType(typeof(Ballot), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Ballot?>> DecryptBallot(int ballotId, string key){
        
        var response = await _repository.GetBallotByIdAsync(ballotId);

        if (response == null)
        {
            return BadRequest();
        }

        var decryptedCandidateId = EncryptionHelper.Decrypt(response.CandidateId, key);
        var decryptedProviderId = EncryptionHelper.Decrypt(response.ProivderId, key);
        var decryptedTimeStamp = EncryptionHelper.Decrypt(response.TimeStamp, key);
        var decryptedSalt = EncryptionHelper.Decrypt(response.Salt, key);

        var originalDataSet = new List<string>{decryptedCandidateId, decryptedProviderId, decryptedTimeStamp, decryptedSalt};
        var originalRH = new MerkleTree(originalDataSet).RootHash;

        var decryptetBallot = new Ballot
        {
            BallotId = response.BallotId,
            CandidateId = decryptedCandidateId,
            ProivderId = decryptedProviderId,
            TimeStamp = decryptedTimeStamp,
            Salt = decryptedSalt,
            DocumentId = response.DocumentId,
            RootHash = originalRH
        };

        return decryptetBallot;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("/GetDocumentInfor/{documentId}")]
    public async Task<DocumentInformation> getDocumentInfo(string documentId)
    {
        return await _repository.GetDocumentInfo(documentId);
    }  
    
}