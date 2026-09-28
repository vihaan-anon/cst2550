using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrimeDashboard.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CrimeDashboard.Services
{
    public class PoliceApiServices
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://data.police.uk/api/")
        };

        //GET: List all the police forces in the UK
        public async Task<List<PoliceForce>> GetForcesAsync()
        {
            string json = await client.GetStringAsync("forces");    
            return JsonConvert.DeserializeObject<List<PoliceForce>>(json);
        }

        //GET: Crime Categories for a given month
        public async Task<List<CrimeCategory>> GetCrimeCategoriesAsync(string date)
        {
            string json = await client.GetStringAsync($"crime-categories?date={date}");
            return JsonConvert.DeserializeObject<List<CrimeCategory>>(json);
        }

        // GET: Street-level crimes near a lat/lng for a given month
        public async Task<List<Crime>> GetCrimesAsync(double lat, double lng, string date)
        {
            string json = await client.GetStringAsync($"crimes-street/all-crime?lat={lat}&lng={lng}&date={date}");
            return JsonConvert.DeserializeObject<List<Crime>>(json);
        }
        // GET: Stop and search data for a given force and month
        public async Task<List<StopAndSearch>> GetStopAndSearchAsync(string forceId, string date)
        {
            string json = await client.GetStringAsync($"stops-force?force={forceId}&date={date}");
            return JsonConvert.DeserializeObject<List<StopAndSearch>>(json);
        }
    }
}