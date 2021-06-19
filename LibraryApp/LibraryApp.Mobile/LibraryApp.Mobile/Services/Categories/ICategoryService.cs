using LibraryApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.Categories
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryById(long id);
        Task<bool> AddNewCategory(Category newCategory);
    }
}
