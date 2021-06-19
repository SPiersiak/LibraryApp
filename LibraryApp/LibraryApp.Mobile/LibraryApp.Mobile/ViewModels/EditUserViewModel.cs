using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        public IUserService _userService => DependencyService.Get<IUserService>();
        public Command SaveChangesCommand { get; }
        private string firstName = Settings.FirstName;
        private string lastName = Settings.LastName;
        private string email = Settings.Email;
        private string password = Settings.Password;
        public EditUserViewModel()
        {
            SaveChangesCommand = new Command(SaveChanges);
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;

                OnPropertyChanged();

            }
        }
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
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
        private async void SaveChanges()
        {
            var role = false;
            if(Settings.Role == "tak")
                role = true;
            else
                role = false;
            User user = new User()
            {
                UserID = long.Parse(Settings.UserId),
                FirstName = firstName,
                LastName = lastName,
                UserName = Settings.UserName,
                Email = email,
                Password = password,
                IsEmployee = role
            };
            var result = await _userService.UpdateUserData(user);
        }
    }
}
