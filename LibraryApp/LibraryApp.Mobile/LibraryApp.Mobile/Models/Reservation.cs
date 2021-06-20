using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Mobile.Models
{
    public class Reservation
    {
        public long ReservationId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public bool IsActive { get; set; }
        public long BookId { get; set; }
        public long UserId { get; set; }
    }
}
