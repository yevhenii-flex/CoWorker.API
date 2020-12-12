using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.ViewModels
{
    public class BookingRecordViewModel
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public UserViewModel ApplicationUser { get; set; }
        public RoomViewModel Room { get; set; }
        public int RoomId { get; set; }
    }
}
