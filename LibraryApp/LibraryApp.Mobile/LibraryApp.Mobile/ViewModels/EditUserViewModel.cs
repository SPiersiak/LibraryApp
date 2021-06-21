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
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        public EditUserViewModel()
        {
            MessagingCenter.Subscribe<LoginViewModel, User>(this, "message", (sender, arg) =>
            {
                FirstName = arg.FirstName;
                LastName = arg.LastName;
                Email = arg.Email;
                Password = arg.Password;
            });
            SaveChangesCommand = new Command(SaveChanges);
            ////FirstName = Settings.FirstName;
            ////LastName = Settings.LastName;
            ////Email = Settings.Email;
            ////Password = Settings.Password;
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
            if(Settings.Role == "Tak")
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
