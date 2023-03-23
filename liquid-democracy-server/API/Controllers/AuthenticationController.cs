using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Idfy;
using Idfy.IdentificationV2;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Core;

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
        [Route("/MitId/Auth")]  
        public Task<IdSession>? AuthenticateUserWithMitId()
        {
            //private readonly IIdentificationV2Service client;

            var moviesApiKey = _config["Signicat:ClientId"];
            var moviesApiKey1 = _config["Signicat:ClientSecret"];
            var client = new IdentificationV2Service(moviesApiKey, moviesApiKey1, 
            new List<OAuthScope>()
            {
                OAuthScope.Identify
            });
            //Console.WriteLine(client);
            //return client;

            var session = client.CreateSessionAsync(new IdSessionCreateOptions()
            {
                AllowedProviders = new List<IdProviderType>()
                {
                    IdProviderType.Mitid,
                    IdProviderType.DkNemid
                },
                RedirectSettings = new RedirectSettings()
                {
                    ErrorUrl = "https://www.google.com/search?q=Error&rlz=1C5CHFA_enDK994DK994&ei=9oLyY6DuLJCUrwSAnY24BA&ved=0ahUKEwigweX2sqL9AhUQyosKHYBOA0cQ4dUDCA8&uact=5&oq=Error&gs_lcp=Cgxnd3Mtd2l6LXNlcnAQAzIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQyBQgAEIAEMgUIABCABDIFCAAQgAQ6CggAEEcQ1gQQsAM6BwgAELADEEM6BwguELADEEM6DQgAEOQCENYEELADGAE6DAguEMgDELADEEMYAjoECC4QQzoLCAAQgAQQsQMQgwE6CAgAEIAEELEDOgUILhCABDoICC4QsQMQgwE6CAgAELEDEIMBOgQIABBDOggILhCABBCxAzoLCC4QgAQQsQMQgwE6CAguEIAEENQCSgQIQRgAUJAOWLYSYKYXaAVwAXgAgAFLiAG7ApIBATWYAQCgAQHIARPAAQHaAQYIARABGAnaAQYIAhABGAg&sclient=gws-wiz-serp",
                    AbortUrl = "https://www.signicat.com#abort",
                    SuccessUrl = "https://localhost:3000/" 
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
        public async Task<SessionInformation> GetSessionInformation(string sessionId){
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
}