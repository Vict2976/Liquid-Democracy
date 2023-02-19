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

        [AllowAnonymous]
        [HttpGet]
        [Route("getIDToken")]
        public Task<IdSession>? getIDToken()
        {
            //private readonly IIdentificationV2Service client;
            var client = new IdentificationV2Service("tfa0dc79b7a8f4949baf7ef3000b23095", "lMMC8ESqzcHrPkxG2bYEb5Y1Zb793jI8y25p8AZSZeTYG4hjMLy5uqVdO0FJRT9J", 
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
                    IdProviderType.NoBankidMobile,
                    IdProviderType.NoBankidNetcentric
                },
                RedirectSettings = new RedirectSettings()
                {
                    ErrorUrl = "https://www.signicat.com#error",
                    AbortUrl = "https://www.signicat.com#abort",
                    SuccessUrl = "https://www.signicat.com#success"
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
                }

            });

            Console.WriteLine(session);

            return session; 
        }

    }
}