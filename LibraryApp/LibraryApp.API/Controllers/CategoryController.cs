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
    ///  kontroler pozwalajacy na dostep do zasobow bazy dancyh przechowujacych kategorie
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        /// <summary>
        ///  konstruktor inicjalizujacy Dependency Incjection dla categoryrepository
        /// </summary>
        /// <param name="categoryRepository">powiazanie z interfejsem categoryRepository</param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// metoda dodajaca nowa kategorie do bazy danych
        /// </summary>
        /// <param name="category">model kategori jaki znajduje sie w bazie danych, zawiera informacje na temat nowej kategori</param>
        /// <returns>zwraca ok jezeli kategoria zostala dodana, jezeli nie BadRequest</returns>
        [HttpPut]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDto category)
        {
            try
            {
                if (String.IsNullOrEmpty(category.CategoryName))
                {
                    return BadRequest();
                }

                var addCategoryResult = await _categoryRepository.AddCategory(category);

                if (addCategoryResult == false)
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
        /// metoda wybierajaca kategorie po id
        /// </summary>
        /// <param name="id">zmienna typu long zawierajaca id szukanej kategori</param>
        /// <returns>zwraca znaleziona kategorie w postacie obiektu Category</returns>
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(long id)
        {
            return _categoryRepository.GetCategoryById(id);
        }
    }
}
