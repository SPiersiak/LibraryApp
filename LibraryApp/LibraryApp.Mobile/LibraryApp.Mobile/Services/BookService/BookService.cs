using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.BookService
{
    public class BookService : IBookService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<Book> GetBookById(long bookId)
        {
            var uri = $"https://192.168.0.107:45456/api/Book/"+ bookId;
            var result = await _requestService.GetAsync<Book>(uri);
            return result;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var uri = $"https://192.168.0.107:45456/api/Book";
            var result = await _requestService.GetAsync<List<Book>>(uri);
            return result;
        }

    }
}
