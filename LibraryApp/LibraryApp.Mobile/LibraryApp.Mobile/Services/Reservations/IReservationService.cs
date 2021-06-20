using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Reservations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.Reservations
{
    public interface IReservationService
    {
        Task<bool> AddNewReservation(ReservationDto reservationDto);
        Task<IEnumerable<Reservation>> GetActiveReservationForUser(long userId);
        Task<IEnumerable<Reservation>> GetInActiveReservationForUser(long userId);
        Task<Reservation> GetActiveReservationForBook(long bookId);

    }
}
