using LibraryApp.Mobile.Services.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> Login(LoginDto input);
        Task<bool> Register(RegisterDto input);
    }
}
