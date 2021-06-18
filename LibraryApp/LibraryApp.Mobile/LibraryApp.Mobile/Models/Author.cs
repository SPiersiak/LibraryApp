using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Mobile.Models
{
    public class Author
    {
        public long AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
    }
}
