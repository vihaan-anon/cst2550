using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrimeDashboard.Models
{
    [Serializable]
    public class CrimeLocation
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        
        [JsonProperty("street")]
        public Street Street { get; set; }
    }
}