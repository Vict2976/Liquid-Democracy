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

    public async Task<Ballot?> CreateAsync(string candidateId, string sessionUuid, string documentId)
    {
        Thread.Sleep(4000);
        var token = await GetToken();
        var documentInformation = await GetDocumentInfo(documentId, token);
        var isSuccesfull = GetStatusForDocument(documentInformation);

        if(!isSuccesfull)
        {
            throw new Exception("The document is not signed");
        }

        var signerUuid = GetUuidForSigner(documentInformation);
        
        if (sessionUuid != signerUuid)
        {
            throw new Exception("This is not the authenticated signer");
        }

        var nonce = GenerateNoncee();
        var timeStamp = DateTime.Now.ToString();

        var originalDataSet = new List<string> { candidateId, sessionUuid, timeStamp, nonce };

        var rootHash = new Security.MerkleTree(originalDataSet).RootHash;
        var ballot = new Ballot
        {
            CandidateId = null,
            Uuid = null,
            TimeStamp = null,
            Nonce = null,
            DocumentId = null,
            RootHash = rootHash
        };

        _context.Ballots.Add(ballot);
        await _context.SaveChangesAsync();

        var keys = RSAEncryption.CreateAndAddKeysToStorage(ballot.BallotId);
        ballot.CandidateId = RSAEncryption.EncryptVoteAsString(candidateId, keys.publicKey);
        ballot.Uuid = RSAEncryption.EncryptVoteAsString(sessionUuid, keys.publicKey);
        ballot.TimeStamp = RSAEncryption.EncryptVoteAsString(timeStamp, keys.publicKey);
        ballot.Nonce = RSAEncryption.EncryptVoteAsString(nonce, keys.publicKey);
        ballot.DocumentId = RSAEncryption.EncryptVoteAsString(documentId, keys.publicKey);

        await _context.SaveChangesAsync();
        return ballot;
    }

    private bool GetStatusForDocument(DocumentInformation documeentInformation)
    {
        var status = documeentInformation.status.documentStatus;
        if (status == "signed"){
            return true;
        }
        return false;
    }

    private string GenerateNoncee()
    {
        int length = 10; //Maybe make length random?
        Random random = new Random();

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))); // generate a random uppercase letter
            stringBuilder.Append(ch);
        }

        string randomString = stringBuilder.ToString();
        return randomString;
    }

    public async Task<Ballot?> GetBallotByIdAsync(int ballotId)
    {
        var ballot = await _context.Ballots.Where(b => b.BallotId == ballotId).Select(b => b).FirstAsync();
        return ballot;
    }

    public async Task<DocumentInformation> GetDocumentInfo(string documentId, string token)
    {
        try
        {
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
        catch (HttpRequestException e)
        {
            return new DocumentInformation();
        }
    }

    //Foreach Ballot, call DecryptBallotById and add to list
    //Foreach Ballot in decryptet list, check for same provider id
    //For entries with same provider ID find newest Timestamp
    //Check for tamper and validity
    //Add to new database, only encryptet candidateId
    public async Task<IEnumerable<Ballot>> FindAndDeleteAllOldVotes()
    {
        var allBallots = await _context.Ballots.ToListAsync();
        var decryptetBallots = new List<Ballot>();

        foreach (var ballot in allBallots)
        {
            try
            {
                var decryptetBallot = await DecryptBallotById(ballot.BallotId);
                decryptetBallots.Add(decryptetBallot);
            }
            catch (Exception e)
            {
                continue;
            }
        }

        var finalBallots = RemoveOlderBallotsWithSameProviderId(decryptetBallots);

        foreach(var ballot in finalBallots){
            await SaveSelectedCandidateInNewTable(ballot.CandidateId);
        }

        return finalBallots;
    }

    public async Task<Ballot> DecryptBallotById(int ballotId)
    {
        var token = await GetToken();
        var encryptedBallot = await GetBallotByIdAsync(ballotId);

        var keys = RSAEncryption.RetrieveKeys(ballotId);

        var decryptedCandidateId = RSAEncryption.DecryptVoteFromString(encryptedBallot.CandidateId, keys.privateKey);
        var decryptedProviderId = RSAEncryption.DecryptVoteFromString(encryptedBallot.Uuid, keys.privateKey);
        var decryptedTimeStamp = RSAEncryption.DecryptVoteFromString(encryptedBallot.TimeStamp, keys.privateKey);
        var decryptedNonce = RSAEncryption.DecryptVoteFromString(encryptedBallot.Nonce, keys.privateKey);
        var decryptedDocumentId = RSAEncryption.DecryptVoteFromString(encryptedBallot.DocumentId, keys.privateKey);

        var documentInformation = await GetDocumentInfo(decryptedDocumentId, token);
        var isSigned = IsDocumentSigned(documentInformation);

        if (encryptedBallot == null || !isSigned)
        {
            return null;
        }

        var originalDataSet = new List<string> { decryptedCandidateId, decryptedProviderId, decryptedTimeStamp, decryptedNonce};
        var originalRH = new Security.MerkleTree(originalDataSet).RootHash;

        if (encryptedBallot.RootHash != originalRH)
        {
            throw new Exception("This ballot has been tamperes with");
        }

        var decryptetBallot = new Ballot
        {
            BallotId = encryptedBallot.BallotId,
            CandidateId = decryptedCandidateId,
            Uuid = decryptedProviderId,
            TimeStamp = decryptedTimeStamp,
            Nonce = decryptedNonce,
            DocumentId = decryptedDocumentId,
            RootHash = originalRH
        };
        return decryptetBallot;
    }

    private async Task SaveSelectedCandidateInNewTable(string candidateId)
    {

        int result = Int32.Parse(candidateId);

        var tally = new Tally
        {
            candidateId = result
        };

        await _context.Tallies.AddAsync(tally);
        await _context.SaveChangesAsync();
    }

    private string GetUuidForSigner(DocumentInformation document)
    {
        var uuid = "";
        document.signers[0].documentSignature.attributes.TryGetValue("mitid.uuid", out uuid);
        return uuid;
    }

    private bool IsDocumentSigned(DocumentInformation document)
    {
        var succes = document.status.documentStatus;
        if (succes == "signed")
        {
            return true;
        }
        return false;
    }

    private async Task<string> GetToken()
    {
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

    private List<Ballot> RemoveOlderBallotsWithSameProviderId(List<Ballot> ballots)
    {
        var providerBallots = new Dictionary<string, List<Ballot>>();

        // Group the ballots by providerId
        foreach (var ballot in ballots)
        {
            if (!string.IsNullOrEmpty(ballot.Uuid))
            {
                if (!providerBallots.ContainsKey(ballot.Uuid))
                {
                    providerBallots.Add(ballot.Uuid, new List<Ballot>());
                }
                providerBallots[ballot.Uuid].Add(ballot);
            }
        }

        var result = new List<Ballot>();

        // Find the most recent ballot among each group of providerId
        foreach (var group in providerBallots.Values)
        {
            var mostRecentBallot = group[0];
            foreach (var ballot in group)
            {
                if (DateTime.Parse(ballot.TimeStamp) > DateTime.Parse(mostRecentBallot.TimeStamp))
                {
                    mostRecentBallot = ballot;
                }
            }
            result.Add(mostRecentBallot);
        }
        return result;
    }

    private async Task<bool> CheckIntegrityByRH()
    {
        //Get ballot from repo
        //Decrypt everything
        //Build merkleTreee
        //Does the roothashes Match?

        throw new NotImplementedException();
    }

    private string Decrypt(string cipherText, string key)
    {
        try
        {
            var decryptedCipherText = EncryptionHelper.Decrypt(cipherText, key);
            return decryptedCipherText;
        }
        catch (Exception e)
        {
            throw new Exception("Ballot has been tampered with");
        }
    }
}
