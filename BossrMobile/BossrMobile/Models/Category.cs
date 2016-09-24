using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BossrMobile.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte ColorR { get; set; }
        public byte ColorG { get; set; }
        public byte ColorB { get; set; }

        [JsonIgnore]
        public List<Creature> Creatures { get; set; }
    }
}
