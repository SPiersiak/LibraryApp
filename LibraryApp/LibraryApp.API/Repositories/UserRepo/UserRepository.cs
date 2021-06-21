using LibraryApp.API.Data;
using LibraryApp.API.Models;
using LibraryApp.API.Repositories.UserRepo.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.UserRepo
{
    /// <summary>
    /// klasa odpowiadajaca za aktualizacje danych uzytkownika
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private LibraryDbContext _libraryDbContext;

        public UserRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public IEnumerable<Models.Reservation> GetMyReservation(long id)
        {
            return _libraryDbContext.Reservations.Where(x => x.UserId == id).ToList();

        }
        /// <summary>
        /// metoda aktualizujaca dane uzytkownika
        /// </summary>
        /// <param name="user">model uzytkownika z danymi do aktualizacji</param>
        /// <returns>zwraca treu jezeli dane uzytkownika zostału zaktualizowane</returns>
        public async Task<bool> Update(User user)
        {
            var findUser = await _libraryDbContext.Users.Where(x => x.UserID == user.UserID).AsNoTracking().FirstOrDefaultAsync();
            if(findUser == null)
            {
                return false;
            }
            _libraryDbContext.Update(user);
            _libraryDbContext.SaveChanges();
            return true;
        }
    }
}
