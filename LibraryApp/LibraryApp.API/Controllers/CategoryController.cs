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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

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
    }
}
