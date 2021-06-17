using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    public class Reservation
    {
        [Key]
        public long ReservationId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("CopiesOfTheBook")]
        public long CopiesId { get; set; }
        public CopiesOfTheBook CopiesOfTheBook { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
