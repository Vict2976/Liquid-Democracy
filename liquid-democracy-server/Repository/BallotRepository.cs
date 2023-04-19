using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Repository;

public class BallotRepository : IBallotRepository
{
    ILiquidDemocracyContext _context;
    private readonly IConfiguration _config;

    public BallotRepository(ILiquidDemocracyContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<Ballot?> CreateAsync(string candidateId, string providerId, string documentId)
    {
        var key = await GetKeyForSigner(documentId);
        var salt = "salty";
        var timeStamp = DateTime.Now.ToString();

        var encryptedCandidateId = EncryptionHelper.Encrypt(candidateId, key);
        var encryptedProviderId = EncryptionHelper.Encrypt(providerId, key);
        var encryptedTimeStamp = EncryptionHelper.Encrypt(timeStamp, key);
        var encryptedSalt = EncryptionHelper.Encrypt(salt, key);


        var originalDataSet = new List<string>{candidateId, providerId, timeStamp, salt};

        var rootHash = new MerkleTree(originalDataSet).RootHash;

        var ballot = new Ballot
            {
                CandidateId = encryptedCandidateId,
                ProivderId = encryptedProviderId,
                TimeStamp = encryptedTimeStamp,
                Salt = encryptedSalt,
                DocumentId = documentId,
                RootHash = rootHash
            };
        
        _context.Ballots.Add(ballot);
        await _context.SaveChangesAsync();
        return ballot;   
    }

    public async Task<Ballot?> GetBallotByIdAsync(int ballotId){
        var ballot = await _context.Ballots.Where(b => b.BallotId == ballotId).Select(b => b).FirstAsync();
        return ballot;
    }

    private async Task<string> GetKeyForSigner(string documentId)
    {
        var documentInformation = await GetDocumentInfo(documentId);
        var symmetricKey =  documentInformation.signers[0].documentSignature.signatureMethodUniqueId;
        return symmetricKey;
    }

    public async Task<DocumentInformation> GetDocumentInfo(string documentId){
        Thread.Sleep(2000); // Pause for 2 seconds
        var token = await GetToken();
        try{
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.idfy.io/signature/documents/" + documentId);
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\n  \"title\": \"As simple as that\",\n  \"description\": \"This is an important document\",\n  \"externalId\": \"ae7b9ca7-3839-4e0d-a070-9f14bffbbf55\",\n  \"dataToSign\": {\n    \"base64Content\": \"VGhpcyB0ZXh0IGNhbiBzYWZlbHkgYmUgc2lnbmVk\",\n    \"fileName\": \"sample.txt\"\n  }, \"contactDetails\": {\n    \"email\": \"test@test.com\"\n  },\n  \"signers\": [ {\n      \"externalSignerId\": \"uoiahsd321982983jhrmnec2wsadm32\",\n      \"redirectSettings\": {\n        \"redirectMode\": \"donot_redirect\"\n      },\n      \"signatureType\": {\n        \"mechanism\": \"pkisignature\"\n      }\n    }\n  ]\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var documentInformation = JsonConvert.DeserializeObject<DocumentInformation>(responseContent);
            return documentInformation;
        }
        catch(HttpRequestException e){
            return new DocumentInformation();
        }
    }

    private async Task<string> GetToken(){
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.signicat.io/oauth/connect/token");
        var clientCredentials = _config["Signicat:ClientCredentials"];
        request.Headers.Add("Authorization", clientCredentials);        
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("grant_type", "client_credentials"));
        var content = new FormUrlEncodedContent(collection);
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var tokenResponse = await response.Content.ReadAsStringAsync();
        var tokenObject = JObject.Parse(tokenResponse);
        var accesToken = tokenObject["access_token"].ToString();
        return accesToken;
    }
}
