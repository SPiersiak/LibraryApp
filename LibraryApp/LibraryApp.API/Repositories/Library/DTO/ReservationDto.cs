using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library.DTO
{
    public class ReservationDto
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
    }
}
