using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrimeDashboard.Models
{
    [Serializable]
    public class OutcomeStatus
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        
        [JsonProperty("date")]
        public string Date { get; set; }
    }
}