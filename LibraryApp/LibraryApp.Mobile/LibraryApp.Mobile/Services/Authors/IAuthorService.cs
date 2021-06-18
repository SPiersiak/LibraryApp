using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Mobile.Models;

namespace LibraryApp.Mobile.Services.Authors
{
    public interface IAuthorService
    {
        Task<Author> GetAuthor(long id);
    }
}
