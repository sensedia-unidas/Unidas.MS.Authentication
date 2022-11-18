using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unidas.MS.Authentication.Application.ViewModels.SalesForce
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken;

        [JsonProperty("instance_url")]
        public string InstanceUrl;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("token_type")]
        public string TokenType;

        [JsonProperty("issued_at")]
        public string IssuedAt;

        [JsonProperty("signature")]
        public string Signature;
    }
}
