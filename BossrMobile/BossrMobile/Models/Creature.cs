using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BossrMobile.Models
{
    public class Creature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }
        public int HoursBetweenEachSpawnMin { get; set; }
        public int HoursBetweenEachSpawnMax { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public List<Location> Locations { get; set; }

        [JsonIgnore]
        public List<Spawn> Spawns { get; set; }
    }
}
