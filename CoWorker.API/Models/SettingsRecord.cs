using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.Models
{
    public class SettingsRecord
    {
        public int Id { get; set; }
        public double Temp { get; set; }
        public bool MusicOn { get; set; }
        public int LightPower { get; set; }
        public int LightTemp { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
