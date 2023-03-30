namespace Server.Controllers;

using Idfy;
using Idfy.Signature;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;


[ApiController]
[Route("[controller]")]
public class SignatureController : ControllerBase
{
    readonly IVoteRepository _repository;
    private readonly IConfiguration _config;

    private readonly ICandidateRepository _candidateRepo;
    private readonly IElectionRepository _electionRepo;

    public SignatureController(IVoteRepository repository, IConfiguration config, ICandidateRepository candidateRepo, IElectionRepository electionRepository)
    {
        _repository = repository;
        _config = config;
        _candidateRepo = candidateRepo;
        _electionRepo = electionRepository;

    }

    [AllowAnonymous]
    [Route("/Verify/{electionId}")]
    [HttpGet]
    [ProducesResponseType(typeof(Election), 200)]
    public async Task<bool> VerifyElection(int electionId){
        var allvotes = await _repository.ReadFromElectionId(electionId);
        foreach(var vote in allvotes){
            var isVerified = await isDocumentSigned(vote.DocumentId);
            if (!isVerified) return false;
        }
        return true;
    }


    private async Task<bool> isDocumentSigned(string documentId){
        var token = await GetToken();
        try{
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.idfy.io/signature/documents/" + documentId);
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\n  \"title\": \"As simple as that\",\n  \"description\": \"This is an important document\",\n  \"externalId\": \"ae7b9ca7-3839-4e0d-a070-9f14bffbbf55\",\n  \"dataToSign\": {\n    \"base64Content\": \"VGhpcyB0ZXh0IGNhbiBzYWZlbHkgYmUgc2lnbmVk\",\n    \"fileName\": \"sample.txt\"\n  }, \"contactDetails\": {\n    \"email\": \"test@test.com\"\n  },\n  \"signers\": [ {\n      \"externalSignerId\": \"uoiahsd321982983jhrmnec2wsadm32\",\n      \"redirectSettings\": {\n        \"redirectMode\": \"donot_redirect\"\n      },\n      \"signatureType\": {\n        \"mechanism\": \"pkisignature\"\n      }\n    }\n  ]\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var signedResponse = await response.Content.ReadAsStringAsync();
            var signedObject = JObject.Parse(signedResponse);
            var status = signedObject["status"];
            var documentStatus = status["documentStatus"].ToString();
            if (documentStatus == "signed"){
                return true;
            }
            return false;
        }catch(HttpRequestException e){
            return false;
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

    
    [AllowAnonymous]
    [Route("/Sign/Candidate/{providerId}/{electionId}/{candidateId}")]
    [HttpGet]
    public async Task<ActionResult> CreateCandidateSign(string providerId, int electionId, int candidateId)
    {

    var clientId = _config["Signicat:ClientId"];
    var clientSecret = _config["Signicat:ClientSecret"];
    var scopes = new[] {OAuthScope.DocumentWrite, OAuthScope.DocumentRead, OAuthScope.DocumentFile};
    var _signatureService = new SignatureService(clientId, clientSecret, scopes);

    //Create specific ballot
    var candidate = await _candidateRepo.GetById(candidateId)!;
    var election = await _electionRepo.GetElectionByIDAsync(electionId);

    MakePdf makePdf = new MakePdf();
    makePdf.createBallot(providerId, election.Name, candidate.Name);


    // Get local file to be signed
    var filePath = Path.Combine("Vote.pdf");
    var data = await System.IO.File.ReadAllBytesAsync(filePath);

    // Configure the signing settings
    var options = new DocumentCreateOptions()
    {
        Title = "Ballot document",

        // Set the redirect and eID-methods
        Signers = new List<SignerOptions>()
        {
            new SignerOptions()
            {
                RedirectSettings = new RedirectSettings()
                {
                    RedirectMode = RedirectMode.Redirect,
                    Error = "https://www.google.com/",
                    Cancel = "https://www.google.com/",
                    Success = "https://www.facebook.com/",
                },
                SignatureType = new SignatureType()
                {
                    Mechanism = SignatureMechanism.Identification,
                    SignatureMethods = new List<SignatureMethod>()
                    {
                        SignatureMethod.Mitid,
                    }

                },
                ExternalSignerId = Guid.NewGuid().ToString(),
            }
        },
        ContactDetails = new ContactDetails()
        {
            Email = "your@company.com"
        },

        // Reference for internal use 
        ExternalId = Guid.NewGuid().ToString(),

        // Optional: Notifications for signers. See API for details
        Notification = new Notification()
        {
            SignRequest = new SignRequest()
            {
                Email = new List<Email>()
                {
                    new Email()
                    {
                        Language = Language.DA,
                        Subject = "Subject text",
                        Text = "The text of the email",
                        SenderName = "Senders Name"
                    }
                }
            }
        },

        // Optional: Retrieve social security number of signer(s)
        Advanced = new Advanced()
        {
            GetSocialSecurityNumber = true,
        },

        // The document to be signed and format
        DataToSign = new DataToSign()
        {
            FileName = "Ballot.pdf",
            Base64Content = Convert.ToBase64String(data),
            Packaging = new Packaging()
            {
                SignaturePackageFormats = new List<SignaturePackageFormat>
                {
                    SignaturePackageFormat.Pades
                }
            }
        }
    };

    // Create document with the settings specified
    var res = await _signatureService.CreateDocumentAsync(options);

    var newSigners = new SignerOptions()
    {
        RedirectSettings = new RedirectSettings()
        {
            RedirectMode = RedirectMode.Redirect,
            Error = "https://www.google.com/",
            Cancel = "https://www.google.com/",
            Success = $"https://localhost:7236/ForCandidate/{providerId}/{electionId}/{candidateId}/{res.DocumentId}",
        },
        SignatureType = new SignatureType()
        {
            Mechanism = SignatureMechanism.Identification,
            SignatureMethods = new List<SignatureMethod>()
            {
                SignatureMethod.Mitid,
            }
        },
        ExternalSignerId = Guid.NewGuid().ToString(),
    };

    var test = res.Signers.FirstOrDefault().Id;

    await _signatureService.UpdateSignerAsync(res.DocumentId, test, newSigners);
    // Redirect user to the URL retrieved from the SDK
    Response.Headers.Add("Location", res.Signers[0].Url);
    return new StatusCodeResult(303);
    }
}