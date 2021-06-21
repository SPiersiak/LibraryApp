using LibraryApp.API.Models;
using LibraryApp.API.Repositories.Library;
using LibraryApp.API.Repositories.Library.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    /// <summary>
    /// kontroler pozwalajacy na dostep do zasobow bazy dancyh przechowujacych rezerwacje
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        /// <summary>
        /// konstruktor inicjalizujacy Dependency Incjection dla reservationrepository
        /// </summary>
        /// <param name="reservationRepository">powiazanie z interfejsem reservationRepository</param>
        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        /// <summary>
        /// metoda dodajaca nowa rezerwacje do bazy danych
        /// </summary>
        /// <param name="reservationDto">model rezerwacji odwzorowany z bazy danych zawierajacy informacje na temat rezerwacji</param>
        /// <returns>zwraca ok jezeli rezerwacja została dodana, jezeli nie BadRequest</returns>
        [HttpPut]
        public IActionResult Addreservation([FromBody] ReservationDto reservationDto)
        {
            var result = _reservationRepository.AddNewReservation(reservationDto.BookId, reservationDto.UserId);
            if (result == false)
                return BadRequest();
            else
                return Ok();
        }

        /// <summary>
        /// metoda koncząca rezerwacje
        /// </summary>
        /// <param name="reservationId">zmienna typu long zawrierajaca id rezerwacji</param>
        /// <returns>zwraca ok jezeli rezerwacja została zakonczona, jezeli nie BadRequest</returns>
        [HttpPost("EndReservation/{reservationId}")]
        public IActionResult EndReservation(long reservationId)
        {
            var result = _reservationRepository.EndReservation(reservationId);
            if (result == false)
                return BadRequest();
            else
                return Ok("Success");
        }

        /// <summary>
        /// metoda przedluzajaca rezerwacje
        /// </summary>
        /// <param name="reservationId">zmienna typu long zawrierajaca id rezerwacji</param>
        /// <returns>zwraca ok jezeli rezerwacja została przedłużona, jezeli nie BadRequest</returns>
        [HttpPost("ExtendReservation/{reservationId}")]
        public IActionResult ExtendReservation(long reservationId)
        {
            var result = _reservationRepository.ExtendReservation(reservationId);
            if (result == false)
                return BadRequest();
            else
                return Ok("Success");
        }

        /// <summary>
        /// metoda wyszukujaca aktywne rezerwacje uzytkownika
        /// </summary>
        /// <param name="userId">zmienna typu long zawrierajaca id uzytkonika</param>
        /// <returns>zwraca liste rezerwacji ktore sa aktywne</returns>
        [HttpGet("ActiveReservation/{userId}")]
        public ActionResult<IEnumerable<Reservation>> GetActiveReservationForUser(long userId)
        {
            return _reservationRepository.GetAllActiveReservationForUser(userId).ToList();
        }

        /// <summary>
        /// metoda wyszukujaca nieaktywne rezerwacje uzytkownika
        /// </summary>
        /// <param name="userId">zmienna typu long zawrierajaca id uzytkonika</param>
        /// <returns>zwraca liste rezerwacji ktore sa nieaktywne</returns>
        [HttpGet("InActiveReservation/{userId}")]
        public ActionResult<IEnumerable<Reservation>> GetInActiveReservationForUser(long userId)
        {
            return _reservationRepository.GetAllInActiveReserwationForUser(userId).ToList();
        }

        /// <summary>
        /// metoda wyszukujaca aktywne rezerwacji dla książki
        /// </summary>
        /// <param name="bookId">zmienna typu long zawrierajaca id ksiazki</param>
        /// <returns>zwraca liste rezerwacji dla danej ksiazki</returns>
        [HttpGet("BookReservation/{bookId}")]
        public ActionResult<Reservation> GetActiveReservationForBook(long bookId)
        {
            return _reservationRepository.GetAllReservationForBookID(bookId);
        }
    }
}
