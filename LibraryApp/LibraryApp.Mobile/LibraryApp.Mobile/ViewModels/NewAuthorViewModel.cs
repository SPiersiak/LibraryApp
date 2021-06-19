using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Authors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class NewAuthorViewModel : BaseViewModel
    {
        public IAuthorService _authorService => DependencyService.Get<IAuthorService>();
        public Command SaveAuthorCommand { get; }
        public Command CancelCommand { get; }
        private string authorFirstName;
        private string authorLastName;
        private string authorBirthDate;
        private string authorCountry;
        public NewAuthorViewModel()
        {
            SaveAuthorCommand = new Command(OnSaveAuthor, AuthorValidateSave);
            CancelCommand = new Command(Cancel);
            this.PropertyChanged +=
                (_, __) => SaveAuthorCommand.ChangeCanExecute();
        }
        public string AuthorFirstName
        {
            get => authorFirstName;
            set => SetProperty(ref authorFirstName, value);
        }
        public string AuthroLastName
        {
            get => authorLastName;
            set => SetProperty(ref authorLastName, value);
        }
        public string AuthroBirthDate
        {
            get => authorBirthDate;
            set => SetProperty(ref authorBirthDate, value);
        }
        public string AuthorCountry
        {
            get => authorCountry;
            set => SetProperty(ref authorCountry, value);
        }
        private void ClearAuthor()
        {
            AuthorFirstName = "";
            AuthroLastName = "";
            AuthroBirthDate = "";
            AuthorCountry = "";
        }
        private bool AuthorValidateSave()
        {
            return !String.IsNullOrWhiteSpace(authorFirstName)
                && !String.IsNullOrWhiteSpace(authorLastName)
                && !String.IsNullOrWhiteSpace(authorBirthDate)
                && !String.IsNullOrWhiteSpace(authorCountry);
        }
        private async void OnSaveAuthor()
        {
            Author author = new Author()
            {
                FirstName = authorFirstName,
                LastName = authorLastName,
                BirthDate = DateTime.Parse(authorBirthDate),
                Country = authorCountry
            };

            var result = await _authorService.AddNewAuthor(author);
            await Application.Current.MainPage.DisplayAlert("Save", "New Author was added", "OK");
            ClearAuthor();
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
