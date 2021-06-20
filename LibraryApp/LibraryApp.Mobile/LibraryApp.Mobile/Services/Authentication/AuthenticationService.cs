using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Authentication.DTO;
using LibraryApp.Mobile.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();
        public async Task<bool> Login(LoginDto input)
        {
            var uri = $"{Settings.Server_Endpoint}Authentication";
            var result = await _requestService.PostAsync<LoginDto, User>(uri, input);
            if (result.UserName == null)
            {
                return false;
            }
            else
            {
                Settings.UserId = result.UserID.ToString();
                Settings.FirstName = result.FirstName;
                Settings.LastName = result.LastName;
                Settings.Email = result.Email;
                Settings.UserName = result.UserName;
                Settings.Password = result.Password;
                Settings.Role = result.IsEmployee ? "Tak" : "Nie";
                return true;
            }
        }

        public async Task<bool> Register(RegisterDto input)
        {
            var uri = $"{Settings.Server_Endpoint}Authentication";
            var result = await _requestService.PutAsync<RegisterDto,string>(uri, input);
            if (result.Equals("istnieje juz taki uzytkowik"))
                return false;
            else 
                return true;
        }
    }
}
