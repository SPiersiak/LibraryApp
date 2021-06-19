using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Authors;
using LibraryApp.Mobile.Services.Categories;
using LibraryApp.Mobile.Services.Publishers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class AddingDataViewModel : BaseViewModel
    {
        public ICategoryService _categoryService => DependencyService.Get<ICategoryService>();
        public IAuthorService _authorService => DependencyService.Get<IAuthorService>();
        public IPublisherService _publisherService => DependencyService.Get<IPublisherService>();

        public AddingDataViewModel()
        {
            SaveAuthorCommand = new Command(OnSaveAuthor, AuthorValidateSave);
            SavePublisherCommand = new Command(OnSavePublisher, PublisherValidateSave);
            SaveCategoryCommand = new Command(OnSaveCategory, CategoryValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveAuthorCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => SavePublisherCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => SaveCategoryCommand.ChangeCanExecute();
        }

        private string publisherName;
        private string publisherDescription;
        private string authorFirstName;
        private string authorLastName;
        private string authorBirthDate;
        private string authorCountry;
        private string categoryName;
        private string categoryDescritpion;
        public Command SaveAuthorCommand { get; }
        public Command SavePublisherCommand { get; }
        public Command SaveCategoryCommand { get; }
        public Command CancelCommand { get; }
        public string PublisherName
        {
            get => publisherName;
            set => SetProperty(ref publisherName, value);
        }
        public string PublisherDesription
        {
            get => publisherDescription;
            set => SetProperty(ref publisherDescription, value);
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
        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
        }
        public string CategoyDescription
        {
            get => categoryDescritpion;
            set => SetProperty(ref categoryDescritpion, value);
        }
        private bool AuthorValidateSave()
        {
            return !String.IsNullOrWhiteSpace(authorFirstName)
                && !String.IsNullOrWhiteSpace(authorLastName)
                && !String.IsNullOrWhiteSpace(authorBirthDate)
                && !String.IsNullOrWhiteSpace(authorCountry);
        }
        private bool PublisherValidateSave()
        {
            return !String.IsNullOrWhiteSpace(publisherName)
                && !String.IsNullOrWhiteSpace(publisherDescription);
        }
        private bool CategoryValidateSave()
        {
            return !String.IsNullOrWhiteSpace(categoryName)
                && !String.IsNullOrWhiteSpace(categoryDescritpion);
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
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }
        private async void OnSavePublisher()
        {
            Publisher publisher = new Publisher()
            {
                PublisherName = publisherName,
                PublisherDescription = publisherDescription
            };

            var result = await _publisherService.AddNewPublisher(publisher);
            await Application.Current.MainPage.DisplayAlert("Save", "New Publisher was added", "OK");
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
           // await Shell.Current.GoToAsync("..");
        }
        private async void OnSaveCategory()
        {
            Category category = new Category()
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescritpion
            };

            var result = await _categoryService.AddNewCategory(category);
            await Application.Current.MainPage.DisplayAlert("Save", "New Category was added", "OK");
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }
        private async void OnCancel()
        {
            //await Shell.Current.GoToAsync("..");
        }
    }
}
