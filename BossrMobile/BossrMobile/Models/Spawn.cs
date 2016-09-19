using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BossrMobile.Models
{
    public class Spawn
    {
        public int Id { get; set; }
        public DateTimeOffset TimeMinUtc { get; set; }
        public DateTimeOffset TimeMaxUtc { get; set; }
        public int WorldId { get; set; }
        public int CreatureId { get; set; }
        public int? LocationId { get; set; }

        [JsonIgnore]
        public World World { get; set; }

        [JsonIgnore]
        public Creature Creature { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }
    }
}
