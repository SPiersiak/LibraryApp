using LibraryApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public interface IReservationRepository
    {
        bool AddNewReservation(long bookId, long userId);
        bool EndReservation(long reservationId);
        bool ExtendReservation(long reservationId);
        Reservation GetAllReservationForBookID(long bookId);
        IEnumerable<Reservation> GetAllActiveReservationForUser(long userId);
        IEnumerable<Reservation> GetAllInActiveReserwationForUser(long userId);

    }
}
