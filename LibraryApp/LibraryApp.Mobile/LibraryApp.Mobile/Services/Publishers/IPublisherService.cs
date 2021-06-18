using LibraryApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.Publishers
{
    public interface IPublisherService
    {
        Task<Publisher> GetPublisherById(long id);
    }
}
