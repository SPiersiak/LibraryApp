using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.Publishers
{
    public class PublisherService : IPublisherService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();

        public async Task<bool> AddNewPublisher(Publisher newPublisher)
        {
            var uri = $"https://192.168.0.107:45456/api/Publisher";
            var result = await _requestService.PutAsync<Publisher>(uri, newPublisher);
            return true;
        }

        public async Task<Publisher> GetPublisherById(long id)
        {
            var uri = $"https://192.168.0.107:45456/api/Publisher/" + id;
            var result = await _requestService.GetAsync<Publisher>(uri);
            return result;
        }
    }
}
