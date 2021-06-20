using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<bool> AddNewAuthor(Author newAuthor)
        {
            var uri = $"{Settings.Server_Endpoint}Author";
            var result = await _requestService.PutAsync<Author>(uri,newAuthor);
            return true;
        }

        public async Task<Author> GetAuthor(long id)
        {
            var uri = $"{Settings.Server_Endpoint}Author/" + id;
            var result = await _requestService.GetAsync<Author>(uri);
            return result;
        }
    }
}
