using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Reservations;
using LibraryApp.Mobile.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class HistoryReservationViewModel : BaseViewModel
    {
        public IReservationService _reservationService => DependencyService.Get<IReservationService>();
        public IBookService _bookService => DependencyService.Get<IBookService>();
        public ObservableCollection<ReservationBookDto> Reservations { get; }
        public Command LoadReservationCommand { get; }
        public HistoryReservationViewModel()
        {
            Reservations = new ObservableCollection<ReservationBookDto>();
            LoadReservationCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Reservations.Clear();
                var items = await _reservationService.GetInActiveReservationForUser(long.Parse(Settings.UserId));
                //DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    var book = await _bookService.GetBookById(item.BookId);
                    var res = new ReservationBookDto()
                    {
                        ReservationId = item.ReservationId,
                        ReservationStart = "Start rezerwacji: " + item.ReservationStart.ToString("yyyy-MM-dd HH:mm"),
                        ReservationEnd = "Koniec Rezerwacji: " + item.ReservationEnd.ToString("yyyy-MM-dd HH:mm"),
                        BookName = "Tytuł: " + book.BookName
                    };
                    Reservations.Add(res);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
