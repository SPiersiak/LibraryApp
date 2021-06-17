using LibraryApp.API.Data;
using LibraryApp.API.Repositories.UserRepo.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.UserRepo
{
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

        public async Task<bool> Update(UserDto user)
        {
            var findUser = await _libraryDbContext.Users.Where(x => x.UserID == user.UserID).FirstOrDefaultAsync();
            if(findUser == null)
            {
                return false;
            }
            _libraryDbContext.Entry(user).State = EntityState.Modified;
            _libraryDbContext.SaveChanges();
            return true;
        }
    }
}
