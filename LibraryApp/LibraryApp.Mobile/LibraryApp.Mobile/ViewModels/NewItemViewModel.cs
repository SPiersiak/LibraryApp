using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.BookService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public IBookService _bookService => DependencyService.Get<IBookService>();
        private string bookName;
        private string description;
        private string isbn;
        private string date;
        private string bookLanguage;
        private string authorId;
        private string publisherId;
        private string categoryId;
        private byte[] bookPhoto;
        public ImageSource _imageSource = "";

        public ICommand TakePicture => new Command(async () => await TakePictureAsync());
        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(bookName)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(isbn)
                && !String.IsNullOrWhiteSpace(date)
                && !String.IsNullOrWhiteSpace(bookLanguage)
                && !String.IsNullOrWhiteSpace(authorId)
                && !String.IsNullOrWhiteSpace(categoryId) 
                && !String.IsNullOrWhiteSpace(publisherId);
        }

        public string BookName
        {
            get => bookName;
            set => SetProperty(ref bookName, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string ISBN
        {
            get => isbn;
            set => SetProperty(ref isbn, value);
        }
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string BookLanguage
        {
            get => bookLanguage;
            set => SetProperty(ref bookLanguage, value);
        }
        public string AuthorId
        {
            get => authorId;
            set => SetProperty(ref authorId, value);
        }
        public string PublisherId
        {
            get => publisherId;
            set => SetProperty(ref publisherId, value);
        }
        public string CategoryId
        {
            get => categoryId;
            set => SetProperty(ref categoryId, value);
        
        }
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }
        public byte[] BookPhoto
        {
            get
            {
                return bookPhoto;
            }
            set
            {
                bookPhoto = value;
                OnPropertyChanged();
            }
        }
        private async Task TakePictureAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    //Nie mozna zrobic zdjecia
                    return;
                }


                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                {
                    Title = "BookPhoto"
                });


                if (photo == null)
                    return;

                var base64 = string.Empty;

                using (var stream = await photo.OpenReadAsync())
                {
                    byte[] photoData = new byte[stream.Length];
                    BookPhoto = photoData;

                    stream.Read(photoData, 0, Convert.ToInt32(stream.Length));


                    base64 = Convert.ToBase64String(photoData);

                    //BookPhoto = base64;
                }



                ImageSource = Xamarin.Forms.ImageSource.FromStream(
           () => new MemoryStream(Convert.FromBase64String(base64)));


            }
            catch (Exception ex)
            {


            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Book book = new Book()
            {
                BookName = BookName,
                Description = Description,
                BookLanguage = BookLanguage,
                ISBN = ISBN,
                ReleaseDate = DateTime.Parse(Date),
                AuthorId = long.Parse(AuthorId),
                PublisherId = long.Parse(PublisherId),
                CategoryId = long.Parse(CategoryId),
                Image = BookPhoto
            };
            var result = await _bookService.AddNewBook(book);
            await Application.Current.MainPage.DisplayAlert("Save", "New Book was added", "OK");
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
