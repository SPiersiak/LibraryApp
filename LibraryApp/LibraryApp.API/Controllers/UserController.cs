using LibraryApp.API.Models;
using LibraryApp.API.Repositories.UserRepo;
using LibraryApp.API.Repositories.UserRepo.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    /// <summary>
    /// kontroler pozwalajacy na dostep do zasobow bazy dancyh przechowujacych uzytkownika
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// konstruktor inicjalizujacy Dependency Incjection dla userrepository
        /// </summary>
        /// <param name="userRepository">powiazanie z interfejsem userRepository</param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// metoda aktualizujaca dane uzytkownika
        /// </summary>
        /// <param name="user"> model uzytkownika zawierajacy zaktualizwane dane</param>
        /// <returns>zwraca ok jezeli dane zostale zaktualizowane, jezeli nie BadRequest</returns>
        [HttpPut]
        public IActionResult UpdateUser(UserDto user)
        {
            User updateUser = new User()
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsEmployee = user.IsEmployee
            };
            var result = _userRepository.Update(updateUser);
            if (result.Result == true)
            {
                return Ok();
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// metoda wyszukujaca rezerwacje uzytkownika
        /// </summary>
        /// <param name="id">zmienna typu long zawierajaca id uzytkownika</param>
        /// <returns>zwraca liste rezerwacji danego uzytkownika</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservation(long id)
        {
            var result = _userRepository.GetMyReservation(id).ToList();
            return result;
        }
    }
}
