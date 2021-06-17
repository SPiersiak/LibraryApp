using LibraryApp.API.Repositories.Library;
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

        [HttpPut("{bookId}, {userId}")]
        public IActionResult Addreservation(long bookId, long userId)
        {
            var result = _reservationRepository.AddNewReservation(bookId, userId);
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
    }
}
