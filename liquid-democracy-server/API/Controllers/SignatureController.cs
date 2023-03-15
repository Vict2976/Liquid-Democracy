namespace Server.Controllers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;

[ApiController]
[Route("[controller]")]
public class SignatureController : ControllerBase
{
    readonly IVoteRepository _repository;
    private readonly IConfiguration _config;

    public SignatureController(IVoteRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<string> Get(){
        var token = await GetToken();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.idfy.io/signature/documents");
        request.Headers.Add("Authorization", "Bearer " + token);
        var content = new StringContent("{\n  \"title\": \"As simple as that\",\n  \"description\": \"This is an important document\",\n  \"externalId\": \"ae7b9ca7-3839-4e0d-a070-9f14bffbbf55\",\n  \"dataToSign\": {\n    \"base64Content\": \"VGhpcyB0ZXh0IGNhbiBzYWZlbHkgYmUgc2lnbmVk\",\n    \"fileName\": \"sample.txt\"\n  }, \"contactDetails\": {\n    \"email\": \"test@test.com\"\n  },\n  \"signers\": [ {\n      \"externalSignerId\": \"uoiahsd321982983jhrmnec2wsadm32\",\n\t  \"redirectSettings\": {\n\t  \t\"redirectMode\": \"redirect\",\n\t\t\"success\": \"https://developer.signicat.io/landing-pages/signing-success.html\",\n\t\t\"cancel\": \"https://developer.signicat.io/landing-pages/something-wrong.html\",\n\t\t\"error\": \"https://developer.signicat.io/landing-pages/something-wrong.html\"\n\t  },\n      \"signatureType\": {\n        \"mechanism\": \"pkisignature\"\n      }\n    }\n  ]\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var parsedResponse = JObject.Parse(responseString);
        var documentID = parsedResponse["documentId"].ToString();
        var signers = parsedResponse["signers"];
        var signedObject = signers[0];
        var url = signedObject["url"].ToString();
        return url;
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