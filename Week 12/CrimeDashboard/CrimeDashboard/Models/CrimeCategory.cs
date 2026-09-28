using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrimeDashboard.Models
{
    public class CrimeCategory
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}