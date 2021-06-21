using LibraryApp.API.Data;
using LibraryApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public ReservationRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public bool AddNewReservation(long bookId, long userId)
        {
            var reservation = new Reservation()
            {
                UserId = userId,
                BookId = bookId,
                ReservationStart = DateTime.Now,
                ReservationEnd = DateTime.Now.AddDays(7)
            };
            _libraryDbContext.Reservations.Add(reservation);
            _libraryDbContext.SaveChanges();
            return true;
        }

        public bool EndReservation(long reservationId)
        {
            var findReservation = _libraryDbContext.Reservations.Where(x => x.ReservationId == reservationId).FirstOrDefault();
            if (findReservation == null)
                return false;
            else
            {
                findReservation.IsActive = false;
                findReservation.ReservationEnd = DateTime.Now;
                _libraryDbContext.Reservations.Update(findReservation);
                _libraryDbContext.SaveChanges();
                return true;
            }
        }

        public bool ExtendReservation(long reservationId)
        {
            var findReservation = _libraryDbContext.Reservations.Where(x => x.ReservationId == reservationId).FirstOrDefault();
            if (findReservation == null)
                return false;
            else
            {
                findReservation.ReservationEnd = findReservation.ReservationEnd.AddDays(7);
                _libraryDbContext.Reservations.Update(findReservation);
                _libraryDbContext.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Reservation> GetAllActiveReservationForUser(long userId)
        {
            DateTime dateTime = DateTime.Now;
            IEnumerable<Reservation> reservations = _libraryDbContext.Reservations.Where(x => x.UserId == userId && x.ReservationEnd >= dateTime).ToList();
            return reservations;
        }

        public IEnumerable<Reservation> GetAllInActiveReserwationForUser(long userId)
        {
            DateTime dateTime = DateTime.Now;
            IEnumerable<Reservation> reservations = _libraryDbContext.Reservations.Where(x => x.UserId == userId && x.ReservationEnd <= dateTime).ToList();
            return reservations;
        }

        public Reservation GetAllReservationForBookID(long bookId)
        {
            DateTime dateTime = DateTime.Now;
            Reservation reservations = _libraryDbContext.Reservations.Where(x => x.BookId == bookId && x.ReservationEnd >= dateTime).FirstOrDefault();
            return reservations;
        }
    }
}
