using FluentValidation.Results;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Authentication.Application.Interfaces.Services;
using Unidas.MS.Authentication.Application.ViewModels;
using Unidas.MS.Authentication.Application.ViewModels.Request;
using Unidas.MS.Authentication.Application.ViewModels.SalesForce;

namespace Unidas.MS.Authentication.Application.Services
{
    public class SalesForceService : ISalesForceService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly AppSettings _appSettings;
        private readonly string SalesForceCache = "AuthenticationTokenSalesForce";

        public SalesForceService(IMemoryCache memoryCache, AppSettings appSettings)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
        }

        public async Task<RetornoToken> Authorize(CredentialsViewModel model)
        {

            try
            {
                var token = RecuperaToken();

                if (string.IsNullOrEmpty(token.Token)) 
                {
                    token = await GeraTokenSalesForce(model);
                }

                return token;
            }
            catch (Exception e)
            {
                //logger de erro
            }

            return null;
        }

        public RetornoToken RecuperaToken()
        {

            if (!_memoryCache.TryGetValue(SalesForceCache, out string cacheValue))
            {
                return new RetornoToken() { Token = cacheValue } ;
            }
            else
            {
                return new RetornoToken() { Token = _memoryCache.Get(SalesForceCache).ToString(), Status = HttpStatusCode.OK } ;
            }
        }

        public async Task<RetornoToken> GeraTokenSalesForce(CredentialsViewModel model)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_appSettings.SalesForce.Url)
            };

            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", model.ClientId),
                    new KeyValuePair<string, string>("client_secret", model.ClientSecret),
                    new KeyValuePair<string, string>("username", model.UserName),
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });

            var response = await client.PostAsync(_appSettings.SalesForce.Url, content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

                if (token != null)
                {
                    SalvaTokenCache(token.AccessToken);

                    return new RetornoToken() { Token = token.AccessToken, Status = HttpStatusCode.OK };
                }

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return new RetornoToken() { Status = HttpStatusCode.BadRequest };
            }
            else if (response.StatusCode == HttpStatusCode.NotFound) 
            {
                return new RetornoToken() { Status = HttpStatusCode.NotFound };
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new RetornoToken() { Status = HttpStatusCode.Unauthorized } ;
            }

            return new RetornoToken();
        }

        private void SalvaTokenCache(string token) 
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(118));

            _memoryCache.Set(SalesForceCache, token, cacheEntryOptions);
        }

    }

}
