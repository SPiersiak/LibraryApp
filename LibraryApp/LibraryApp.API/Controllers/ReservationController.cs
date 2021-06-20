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
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpPut]
        public IActionResult Addreservation([FromBody] ReservationDto reservationDto)
        {
            var result = _reservationRepository.AddNewReservation(reservationDto.BookId, reservationDto.UserId);
            if (result == false)
                return BadRequest();
            else
                return Ok();
        }
        [HttpPost("{reservationId}")]
        public IActionResult EndReservation(long reservationId)
        {
            var result = _reservationRepository.EndReservation(reservationId);
            if (result == false)
                return BadRequest();
            else
                return Ok();
        }

        [HttpPost]
        public IActionResult ExtendReservation(long reservationId)
        {
            var result = _reservationRepository.ExtendReservation(reservationId);
            if (result == false)
                return BadRequest();
            else
                return Ok();
        }
        [HttpGet("ActiveReservation/{userId}")]
        public ActionResult<IEnumerable<Reservation>> GetActiveReservationForUser(long userId)
        {
            return _reservationRepository.GetAllActiveReservationForUser(userId).ToList();
        }
        [HttpGet("InActiveReservation/{userId}")]
        public ActionResult<IEnumerable<Reservation>> GetInActiveReservationForUser(long userId)
        {
            return _reservationRepository.GetAllInActiveReserwationForUser(userId).ToList();
        }
        [HttpGet("BookReservation/{bookId}")]
        public ActionResult<Reservation> GetActiveReservationForBook(long bookId)
        {
            return _reservationRepository.GetAllReservationForBookID(bookId);
        }
    }
}
