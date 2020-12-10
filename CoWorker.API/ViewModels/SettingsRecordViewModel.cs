using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.ViewModels
{
    public class SettingsRecordViewModel
    {
        public int Id { get; set; }
        public double Temp { get; set; }
        public bool MusicOn { get; set; }
        public int LightPower { get; set; }
        public int LightTemp { get; set; }
        public UserViewModel ApplicationUser { get; set; }
    }
}
