using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrimeDashboard.Models
{
    [Serializable]
    public class Crime
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("location")]
        public CrimeLocation Location { get; set; }

        [JsonProperty("outcome_status")]
        public OutcomeStatus OutcomeStatus { get; set; }

        public string StreetName => Location?.Street?.Name ?? "Unknown"; //Lambda expression to get the street name, handling null values gracefully
        public string OutcomeText => OutcomeStatus?.Category ?? "No Outcome recorded";
        public string CategoryDisplay => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Category.Replace("-", " ") ?? "");
    }
}