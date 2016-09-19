using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BossrMobile.Models
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }
        public int LastDayDeaths { get; set; }
        public int LastDayKills { get; set; }
        public DateTimeOffset LastScrapeTime { get; set; }

        [JsonIgnore]
        public List<Spawn> Spawns { get; set; }
    }
}