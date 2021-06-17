using LibraryApp.API.Models;
using LibraryApp.API.Repositories.UserRepo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<bool> Update(UserDto user);
        IEnumerable<Reservation> GetMyReservation(long id);
    }
}
