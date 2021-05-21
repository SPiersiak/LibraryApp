using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    public class Publisher
    {
        [Key]
        public long PublisherId { get; set; }
        [Required]
        public string PublisherName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
