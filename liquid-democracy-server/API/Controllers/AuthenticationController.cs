using Idfy;
using Idfy.IdentificationV2;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Server
{

    [Route("[controller]")]
    [ApiController]
    public class AuthenticationApiController : Controller
    {

        private readonly IConfiguration _config;

        public AuthenticationApiController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/MitId/Auth/{electionId}")]
        public Task<IdSession>? AuthenticateUserWithMitId(int electionId)
        {

            var moviesApiKey = _config["Signicat:ClientId"];
            var moviesApiKey1 = _config["Signicat:ClientSecret"];
            var client = new IdentificationV2Service(moviesApiKey, moviesApiKey1,
            new List<OAuthScope>()
            {
                OAuthScope.Identify
            });

            var session = client.CreateSessionAsync(new IdSessionCreateOptions()
            {
                AllowedProviders = new List<IdProviderType>()
                {
                    IdProviderType.Mitid,
                    IdProviderType.DkNemid
                },
                RedirectSettings = new RedirectSettings()
                {
                    ErrorUrl = "https://www.google.com/search?q=error&oq=error&aqs=chrome..69i57j35i39l2j0i512l2j69i60l3.1236j0j4&sourceid=chrome&ie=UTF-8",
                    AbortUrl = "https://www.signicat.com#abort",
                    SuccessUrl = "http://localhost:3000/Election/sign/" + electionId
                },
                ExternalReference = Guid.NewGuid().ToString("n"),
                Flow = IdSessionFlow.Redirect,
                Language = Language.En,
                Include = new List<Include>()
                {
                    Include.Nin,
                    Include.Name
                },
                Ui = new UiSettings()
                {
                    ColorTheme = ColorTheme.Default,
                    ThemeMode = ThemeMode.Dark
                },
            });

            return session;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/MitId/Session/{sessionId}")]
        public async Task<SessionInformation> GetSessionInformation(string sessionId)
        {
            var token = await GetToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.idfy.io/identification/v2/sessions/" + sessionId);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var sessionInformation = JsonConvert.DeserializeObject<SessionInformation>(content);
            return sessionInformation;
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
    }
}