using LibraryApp.Mobile.Services.Authentication;
using LibraryApp.Mobile.Services.Authentication.DTO;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

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
                //MessagingCenter.Send<LoginViewModel>(this,
                //(Settings.Role == "Tak") ? "Tak" : "Nie");
                var x = new AppViewModel();
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
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
