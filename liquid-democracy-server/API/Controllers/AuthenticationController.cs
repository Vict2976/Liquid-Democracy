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

namespace Server
{

    [Route("authentication-session")]
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
        [Route("/MidId/Authenticate/{userId}")]  
        public Task<IdSession>? AuthenticateUserWithMitId(int userId)
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
                    SuccessUrl = "https://localhost:7236/AddMitIDSession/" + userId,
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
        [Route("/VoteForCandidate/{userId}/{electionId}/{candidateId}")]  
        public Task<IdSession>? getIDTokenForCandidate(int userId, int electionId, int? candidateId)
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
                    SuccessUrl = $"https://localhost:7236/ForCandidate/{userId}/{electionId}/{candidateId}/",
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
        [Route("/VoteForDelegate/{userId}/{electionId}/{delegateId}")]  
        public Task<IdSession>? IDTokenForDelegate(int userId, int electionId, int? delegateId)
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
                    SuccessUrl = $"https://localhost:7236/ForDelegate/{userId}/{electionId}/{delegateId}",
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

    }
}
