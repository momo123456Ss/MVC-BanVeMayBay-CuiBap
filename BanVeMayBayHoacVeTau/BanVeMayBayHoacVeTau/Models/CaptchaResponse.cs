using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeMayBayHoacVeTau.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }
        [JsonProperty("error-codes")]
        public List<string> Error { get; set; }
    }
}