using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Authors;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Categories;
using LibraryApp.Mobile.Services.Publishers;
using LibraryApp.Mobile.Services.Reservations;
using LibraryApp.Mobile.Services.Reservations.Dto;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public IBookService _bookService => DependencyService.Get<IBookService>();
        public IAuthorService _authorService => DependencyService.Get<IAuthorService>();
        public IPublisherService _publisherService => DependencyService.Get<IPublisherService>();
        public ICategoryService _categoryService => DependencyService.Get<ICategoryService>();
        public IReservationService _reservationService => DependencyService.Get<IReservationService>();
        private long bookId;
        private string title;
        private string description;
        private string authorName;
        private string category;
        private string publisher;
        private string isbn;
        private string date;
        private string reservationInfo;
        private bool doReservation;
        private string prolongationInfo;
        private bool doProlangate;
        private long reservationId;
        private bool resInfo;
        public long Id { get; set; }
        public ImageSource _imageSource = "";
        public Command ReservationCommand { get; }
        public Command ProlongateCommand { get; }
        public Command EndReservationCommand { get; }
        public ItemDetailViewModel()
        {
            ReservationCommand = new Command(NewReservation);
            ProlongateCommand = new Command(AddProlongate);
            EndReservationCommand = new Command(EndReservation);
        }
        public string Text
        {
            get => title;
            set => SetProperty(ref title, value);
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
        public string AuthorName
        {
            get => authorName;
            set => SetProperty(ref authorName, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Publisher
        {
            get => publisher;
            set => SetProperty(ref publisher, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string ReservationInfo
        {
            get => reservationInfo;
            set => SetProperty(ref reservationInfo, value);
        }
        public bool DoReservation
        {
            get => doReservation;
            set => SetProperty(ref doReservation, value);
        }
        public string ProlongationInfo
        {
            get => prolongationInfo;
            set => SetProperty(ref prolongationInfo, value);
        }
        public bool DoProlongate
        {
            get => doProlangate;
            set => SetProperty(ref doProlangate, value);
        }
        public bool ResInfo
        {
            get => resInfo;
            set => SetProperty(ref resInfo, value);
        }

        public long BookId
        {
            get
            {
                return bookId;
            }
            set
            {
                bookId = value;
                LoadItemId(value);
            }
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

        public async void LoadItemId(long itemId)
        {
            try
            {
                
                var item = await _bookService.GetBookById(itemId);
                var author = await _authorService.GetAuthor(item.AuthorId);
                var pub = await _publisherService.GetPublisherById(item.PublisherId);
                var cate = await _categoryService.GetCategoryById(item.CategoryId);
                var rese = await _reservationService.GetActiveReservationForBook(item.BookId);
                AuthorName = author.FirstName + " " + author.LastName;
                Id = item.BookId;
                Text = item.BookName;
                Description = item.Description;
                Publisher = pub.PublisherName;
                Category = cate.CategoryName;
                ISBN = item.ISBN;
                Date = item.ReleaseDate.ToString();
                ImageSource = Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(item.Image))));
                if(Settings.Role == "Anonnymous")
                {
                    DoProlongate = false;
                    DoReservation = false;
                    ResInfo = false;

                }
                else
                {
                    if (rese == null)
                    {
                        ResInfo = true;
                        ReservationInfo = "Ta książka nie jest zarezerwowana";
                        DoReservation = true;
                    }
                    else
                    {
                        var day = rese.ReservationEnd - DateTime.Now;
                        if (rese.UserId == long.Parse(Settings.UserId) && day.Days < 2)
                        {
                            ProlongationInfo = "Ta książka jest zarezerwowana przez ciebie lecz nie można jej juz przedłuzyć " +
                                "ponieważ za 2 dni konczy sie rezerwacja";
                            DoProlongate = false;
                            ResInfo = false;
                        }
                        else if (rese.UserId == long.Parse(Settings.UserId) && day.Days > 2)
                        {
                            var data = rese.ReservationEnd.ToString("yyyy-MM-dd HH:mm:ss");
                            ProlongationInfo = "Ta książka jest zarezerwowana przez ciebie i istnieje " +
                                "możliwość przedłużenia rezerwacji\n" +
                                "Koniec rezerwacji: " + data; ;
                            reservationId = rese.ReservationId;
                            DoProlongate = true;
                            ResInfo = false;
                        }
                        else
                        {
                            ReservationInfo = "Ta książka jest zarezerwowana";
                            ResInfo = true;
                            DoReservation = false;
                        }
                        //DoReservation = false;
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        private async void NewReservation()
        {
            ReservationDto reservationDto = new ReservationDto()
            {
                BookId = BookId,
                UserId = long.Parse(Settings.UserId)
            };
            await _reservationService.AddNewReservation(reservationDto);
        }
        private async void AddProlongate()
        {
            await _reservationService.BookProlongate(reservationId);
        }
        private async void EndReservation()
        {
            await _reservationService.EndReservation(reservationId);
        }
    }
}
