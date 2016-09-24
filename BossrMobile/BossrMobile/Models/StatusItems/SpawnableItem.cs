using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BossrMobile.Models.StatusItems
{
    public class SpawnableItem
    {
        public string CreatureName { get; set; }
        public string TimeLeft { get; set; }
        public int MissedSpawns { get; set; }
        public string CategoryName { get; set; }
        public Color CategoryColorRgb { get; set; }
    }
}
