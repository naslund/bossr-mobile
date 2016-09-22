using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BossrMobile.Models;
using Newtonsoft.Json;

namespace BossrMobile.Services
{
    public class RestService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly Uri uri = new Uri("https://bossr.azurewebsites.net/api");

        public async Task<IEnumerable<World>> GetWorldsAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/worlds");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<World>>(content).OrderBy(x => x.Name);
            }

            return null;
        }

        public async Task<IEnumerable<Spawn>> GetLatestWorldSpawnsAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/worlds/{id}/spawns/latest");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Spawn>>(content);
            }

            return null;
        }

        public async Task<IEnumerable<Creature>> GetCreaturesAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/creatures");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Creature>>(content).Where(x => x.Monitored).OrderBy(x => x.Name);
            }

            return null;
        }
    }
}
