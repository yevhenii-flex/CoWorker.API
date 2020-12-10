using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public SettingsRecord Settings { get; set; }

    }
}
