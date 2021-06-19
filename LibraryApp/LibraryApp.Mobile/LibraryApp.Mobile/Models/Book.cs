using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Mobile.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string BookName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string BookLanguage { get; set; }
        public byte[] Image { get; set; }
        public string ISBN { get; set; }
        public long AuthorId { get; set; }
        public long PublisherId { get; set; }
        public long CategoryId { get; set; }
    }
}
