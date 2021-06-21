using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    /// <summary>
    /// model książki ktory odwzorowuje jego tabele w bazie danych
    /// </summary>
    public class Book
    {
        [Key]
        public long BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        public DateTime ReleaseDate { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public string BookLanguage { get; set; }
        public byte[] Image { get; set; }
        public string ISBN { get; set; }

        [ForeignKey("Author")]
        public long AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Publisher")]
        public long PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Borrowed> Borroweds { get; set; }

    }
}
