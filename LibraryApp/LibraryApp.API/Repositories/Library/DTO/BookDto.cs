using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library.DTO
{
    public class BookDto
    {
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
