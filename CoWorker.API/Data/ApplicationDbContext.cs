using System;
using System.Collections.Generic;
using System.Text;
using CoWorker.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoWorker.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BookingRecord> BookingRecords { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<SettingsRecord> SettingsRecords { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
