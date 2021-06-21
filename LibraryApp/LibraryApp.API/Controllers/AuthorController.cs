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
    /// kontroler pozwalajacy na dodawanie i wybieranie autora
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        /// <summary>
        /// konstruktor inicjalizujacy Dependency Incjection dla authorrepository
        /// </summary>
        /// <param name="authorRepository">powiazanie z interfejsem authorRepository</param>
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        /// <summary>
        /// metoda dodajaca nowego autora do bazy danych
        /// </summary>
        /// <param name="author">model autora jaki znajduje sie w bazie danych</param>
        /// <returns>ok jezeli autor został poprawnie dodany do bay danych, w innym przypadku BadRequest</returns>
        [HttpPut]
        public async Task<IActionResult> AddNewAuthor([FromBody] AuthorDto author)
        {
            try
            {
                if (String.IsNullOrEmpty(author.FirstName) || String.IsNullOrEmpty(author.LastName))
                {
                    return BadRequest();
                }

                var addAuthorResult = await _authorRepository.AddAuthor(author);

                if (addAuthorResult == false)
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
        /// metoda wyszukujaca i zwracajacego autora po id
        /// </summary>
        /// <param name="id">zmienna typu long zawierajaca id autora</param>
        /// <returns>zostanie zwrocony obiekt autora znajdujacy sie w bazie danych</returns>
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(long id)
        {
            return _authorRepository.GetAuthorById(id);
        }
    }
}
