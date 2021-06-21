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
    /// kontroler pozwalajacy na dostep do zasobow bazy dancyh przechowujacych wydawców
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        /// <summary>
        ///  konstruktor inicjalizujacy Dependency Incjection dla publisherrepository
        /// </summary>
        /// <param name="publisherRepository">powiazanie z interfejsem publisherRepository</param>
        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        /// <summary>
        /// metoda dodajaca nowego wydawce do bazy danych
        /// </summary>
        /// <param name="publisher">model wydawcy jaki znajduje sie w bazie danych, zawiera informacje na temat nowego wydawcy</param>
        /// <returns>zwraca ok jezeli wydawca zostala dodana, jezeli nie BadRequest</returns>
        [HttpPut]
        public async Task<IActionResult> AddNewPublisher([FromBody] PublisherDto publisher)
        {
            try
            {
                if (String.IsNullOrEmpty(publisher.PublisherName))
                {
                    return BadRequest();
                }

                var addPublisherResult = await _publisherRepository.AddPublisher(publisher);

                if (addPublisherResult == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// metoda wybierajaca wydawce po id
        /// </summary>
        /// <param name="id">zmienna typu long zawierajaca id szukanego wydawcy</param>
        /// <returns>zwraca znalezionego wydawce w postacie obiektu Publisher</returns>
        [HttpGet("{id}")]
        public ActionResult<Publisher> GetPublisherById(long id)
        {
            return _publisherRepository.GetPublisherById(id);
        }
    }
}
