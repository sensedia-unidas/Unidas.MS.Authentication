﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unidas.MS.Authentication.Application.ViewModels.SalesForce
{
    public class RetornoToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
