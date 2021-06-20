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
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

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
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return _bookRepository.GetAllBooks().ToList();
        }

        [HttpGet("GetByName/{name}")]
        public ActionResult<IEnumerable<Book>> FindBookByName(string name)
        {
            return _bookRepository.FindBook(name).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            return _bookRepository.GetBookById(id);
        }
    }
}
