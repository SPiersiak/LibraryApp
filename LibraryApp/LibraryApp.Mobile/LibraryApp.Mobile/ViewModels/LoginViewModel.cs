using LibraryApp.Mobile.Services.Authentication;
using LibraryApp.Mobile.Services.Authentication.DTO;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public IAuthenticationService _authenticationService => DependencyService.Get<IAuthenticationService>();
        public Command LoginCommand { get; }
        string loginName = "";
        string password = "";
        public LoginViewModel()
        {
            LoginCommand = new Command(LogIn,ValidateSave);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }
        public string LoginName
        {
            get => loginName;
            set => SetProperty(ref loginName, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(loginName)
                && !String.IsNullOrWhiteSpace(password);
        }
        private async void LogIn()
        {
            IsBusy = true;
            LoginDto login = new LoginDto()
            {
                UserName = loginName,
                Password = password
            };
            var result = await _authenticationService.Login(login);
            if(result == true)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Sukces", "Dane Porawne", "OK");
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nie poprawne dane", "OK");
            }
        }

    }
}
