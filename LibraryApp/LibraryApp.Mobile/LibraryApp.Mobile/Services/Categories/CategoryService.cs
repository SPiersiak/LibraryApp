using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<bool> AddNewCategory(Category newCategory)
        {
            var uri = $"{Settings.Server_Endpoint}Category";
            var result = await _requestService.PutAsync<Category>(uri,newCategory);
            return true;
        }

        public async Task<Category> GetCategoryById(long id)
        {
            var uri = $"{Settings.Server_Endpoint}Category/" + id;
            var result = await _requestService.GetAsync<Category>(uri);
            return result;
        }
    }
}
