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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

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

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(long id)
        {
            return _authorRepository.GetAuthorById(id);
        }
    }
}
