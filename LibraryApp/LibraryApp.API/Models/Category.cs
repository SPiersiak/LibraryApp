using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    /// <summary>
    /// model kategori ktory odwzorowuje jego tabele w bazie danych
    /// </summary>
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }
        [Required]
        [MaxLength(20)]
        public string CategoryName { get; set; }
        [MaxLength(500)]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
