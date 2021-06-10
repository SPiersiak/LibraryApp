using LibraryApp.API.Data;
using LibraryApp.API.Models;
using LibraryApp.API.Repositories.Library.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public CategoryRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<bool> AddCategory(CategoryDto item)
        {
            var findCategoryDuplicate = await _libraryDbContext.Categories.Where(x => x.CategoryName.Equals(item.CategoryName)).FirstOrDefaultAsync();

            if (findCategoryDuplicate != null)
            {
                return false;
                throw new Exception("Istnieje juz taka kategoria");
            }
            var category = new Category()
            {
                CategoryName = item.CategoryName,
                CategoryDescription = item.CategoryDescription
            };

            await _libraryDbContext.Categories.AddAsync(category);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
