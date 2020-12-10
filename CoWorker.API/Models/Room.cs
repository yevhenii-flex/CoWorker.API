using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.Models
{
    public class Room
    {
        public Room()
        {
            CurrentSettingsId = 1;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public SettingsRecord CurrentSettings { get; set; }
        public int? CurrentSettingsId { get; set; }
    }
}
