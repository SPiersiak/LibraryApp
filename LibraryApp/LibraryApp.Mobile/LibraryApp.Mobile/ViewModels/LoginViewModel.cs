using LibraryApp.Mobile.Services.Authentication;
using LibraryApp.Mobile.Services.Authentication.DTO;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using LibraryApp.Mobile.Models;

namespace LibraryApp.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public IAuthenticationService _authenticationService => DependencyService.Get<IAuthenticationService>();
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command AnonnymousCommand { get; }
        private string loginName = "";
        private string password = "";
        public LoginViewModel()
        {
            LoginCommand = new Command(LogIn,ValidateSave);
            RegisterCommand = new Command(Register);
            AnonnymousCommand = new Command(Anonnymous);
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
                MessagingCenter.Send<LoginViewModel>(this,
                (Settings.Role == "Tak") ? "Tak" : "Nie");
                var user = Settings.FirstName + " " +Settings.LastName;
                MessagingCenter.Send<LoginViewModel,string>(this, "name",
                user);
                MessagingCenter.Send<LoginViewModel, string>(this, "role",
                Settings.Role);
                var userdate = new User() {
                    FirstName = Settings.FirstName, 
                    LastName = Settings.LastName,
                    Email = Settings.Email,
                    Password = Settings.Password };
                MessagingCenter.Send<LoginViewModel, User>(this, "message",
                 userdate);
                var x = new AppViewModel();
                var z = new EditUserViewModel();
                var y = new AboutViewModel();
                //await Application.Current.MainPage.DisplayAlert("Sukces", "Dane Porawne", "OK");
                //await Task.Delay(400);
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Błąd", "Nie poprawne dane", "OK");
            }
        }
        private async void Register()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
        private async void Anonnymous()
        {
            Settings.Role = "Anonnymous";
            MessagingCenter.Send<LoginViewModel>(this,
                (Settings.Role == "Tak") ? "Tak" : "Anonnymous");
            MessagingCenter.Send<LoginViewModel, string>(this, "role",
                Settings.Role);
            var x = new AppViewModel();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
