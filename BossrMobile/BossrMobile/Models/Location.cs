using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BossrMobile.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public byte PosZ { get; set; }
        public int CreatureId { get; set; }

        [JsonIgnore]
        public Creature Creature { get; set; }

        [JsonIgnore]
        public List<Spawn> Spawns { get; set; }
    }
}
