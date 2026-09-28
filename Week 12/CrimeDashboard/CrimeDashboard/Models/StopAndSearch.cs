using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CrimeDashboard.Models
{
    [Serializable]
    public class StopAndSearch
    {
        [JsonProperty("age_range")]
        public string AgeRange { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("self_defined_ethnicity")]
        public string SelfDefinedEthnicity { get; set; }

        [JsonProperty("officer_defined_ethnicity")]
        public string OfficerDefinedEthnicity { get; set; }

        [JsonProperty("object_of_search")]
        public string ObjectOfSearch { get; set; }

        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("datetime")]
        public string DateTime { get; set; }

        [JsonProperty("legislation")]
        public string Legislation { get; set; }

        // Computed property for display
        public string OutcomeDisplay =>
            string.IsNullOrEmpty(Outcome) ? "No outcome" : Outcome;

    }
}