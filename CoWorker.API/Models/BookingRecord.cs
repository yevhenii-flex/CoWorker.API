using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.Models
{
    public class BookingRecord
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
