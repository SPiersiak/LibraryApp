using LibraryApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.LoginRegister
{
    public interface ILoginRegisterRepository
    {
        Task<User> LoginAsync(string userName, string password);
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string userName, string password);
    }
}
