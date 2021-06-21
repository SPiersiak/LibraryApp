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
    /// kontroler pozwalajacy na dostep do zasobow bazy dancyh przechowujacych książki
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        /// <summary>
        ///  konstruktor inicjalizujacy Dependency Incjection dla bookrepository
        /// </summary>
        /// <param name="bookRepository">powiazanie z interfejsem bookRepository</param>
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// metoda dodajaca nowa ksiazke do bazy danch
        /// </summary>
        /// <param name="book">model ksiazki jaki znajduje sie w bazie danych, zawiera informacje na temat nowej ksiazki</param>
        /// <returns>zwraca ok jezeli ksiazka została zapisana jezeli nie BadRequest</returns>
        [HttpPut]
        public async Task<IActionResult> AddNewBook([FromBody] BookDto book)
        {
            try
            {
                if (String.IsNullOrEmpty(book.BookName) || String.IsNullOrEmpty(book.BookLanguage) || book.AuthorId == 0 || book.PublisherId == 0 || book.CategoryId == 0)
                {
                    return BadRequest();
                }

                var addBookResult = await _bookRepository.AddBook(book);

                if (addBookResult == false)
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
        /// metoda wybierajaca ksiazki z bazy danych
        /// </summary>
        /// <returns>zwara liste ksiazek ktore sa zapisane w bazie dancyh</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return _bookRepository.GetAllBooks().ToList();
        }

        /// <summary>
        /// metoda wyszukujaca ksiazki po nazwie
        /// </summary>
        /// <param name="name">zmienna w postaci ciagu znakow zawierajaca szukane nazwy ksiazek</param>
        /// <returns>zwraca list ksiazek ktorych nazwa zawierała szukana fraze</returns>
        [HttpGet("GetByName/{name}")]
        public ActionResult<IEnumerable<Book>> FindBookByName(string name)
        {
            return _bookRepository.FindBook(name).ToList();
        }

        /// <summary>
        /// metoda wyszukujaca książkę po id
        /// </summary>
        /// <param name="id">zmienna typu long zawierajaca id kasizki</param>
        /// <returns>zwraca książke ktora posiada szukany numer Id, w postaci obietku book</returns>
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            return _bookRepository.GetBookById(id);
        }
    }
}
