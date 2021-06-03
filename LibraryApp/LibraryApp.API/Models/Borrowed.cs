using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    public class Borrowed
    {
        [Key]
        public long BorrowID { get; set; }
        public TimeSpan BorrowStart { get; set; }
        public TimeSpan BorrowEnd { get; set; }
        public int Prolongation { get; set; }

        [ForeignKey("CopiesOfTheBook")]
        public long CopiesId { get; set; }
        public CopiesOfTheBook CopiesOfTheBook { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
