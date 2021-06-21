using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Publishers;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class NewPublisherViewModel : BaseViewModel
    {
        public IPublisherService _publisherService => DependencyService.Get<IPublisherService>();
        public Command SavePublisherCommand { get; }
        public Command CancelCommand { get; }
        private string publisherName;
        private string publisherDescription;
        public NewPublisherViewModel()
        {
            SavePublisherCommand = new Command(OnSavePublisher, PublisherValidateSave);
            CancelCommand = new Command(Cancel); 
            this.PropertyChanged +=
                (_, __) => SavePublisherCommand.ChangeCanExecute();
        }
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
        private void ClearPublisher()
        {
            PublisherName = "";
            PublisherDesription = "";
        }
        private bool PublisherValidateSave()
        {
            return !String.IsNullOrWhiteSpace(publisherName)
                && !String.IsNullOrWhiteSpace(publisherDescription);
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
            ClearPublisher();
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(AddingDataPage)}");
        }
        private async void Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
