using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    public class CopiesOfTheBook
    {
        [Key]
        public long CopiesId { get; set; }
        public string Condition { get; set; }

        [ForeignKey("Book")]
        public long BookId { get; set; }
        public Book Book { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Borrowed> Borroweds { get; set; }
    }
}
