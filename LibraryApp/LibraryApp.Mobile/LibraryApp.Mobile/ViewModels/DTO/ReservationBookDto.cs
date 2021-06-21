using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Mobile.ViewModels.DTO
{
    public class ReservationBookDto
    {
        public long ReservationId { get; set; }
        public string ReservationStart { get; set; }
        public string ReservationEnd { get; set; }
        public string BookName { get; set; }
    }
}
