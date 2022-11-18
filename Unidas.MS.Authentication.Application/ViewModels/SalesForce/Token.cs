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
        
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("instance_url")]
        public string InstanceUrl { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("issue_at")]
        public string IssueAt { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}
