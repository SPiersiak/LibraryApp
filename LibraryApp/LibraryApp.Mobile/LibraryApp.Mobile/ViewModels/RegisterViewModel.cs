using LibraryApp.Mobile.Services.Authentication;
using LibraryApp.Mobile.Services.Authentication.DTO;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public IAuthenticationService _authenticationService => DependencyService.Get<IAuthenticationService>();
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }
        private string firstName;
        private string lastName;
        private string userName;
        private string email;
        private string password;
        public RegisterViewModel()
        {
            RegisterCommand = new Command(Register,ValidateSave);
            this.PropertyChanged +=
                (_, __) => RegisterCommand.ChangeCanExecute();
        }
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(firstName)
                && !String.IsNullOrWhiteSpace(lastName)
                && !String.IsNullOrWhiteSpace(userName)
                && !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(password);
        }
        private void Clear()
        {
            FirstName = "";
            LastName = "";
            UserName = "";
            Email = "";
            Password = "";
        }
        private async void Register()
        {
            RegisterDto registerDto = new RegisterDto()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };
            var result = await _authenticationService.Register(registerDto);
            if(result)
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            else
            {
                Clear();
                await Application.Current.MainPage.DisplayAlert("Błąd", "Istnieje użytkownik z takim loginem", "OK");
            }
        }
    }
}
