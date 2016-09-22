using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossrMobile.Models
{
    public class SpawnableStatus
    {
        public string CreatureName { get; set; }
        public string TimeLeft { get; set; }
        public int MissedSpawns { get; set; }
    }
}
