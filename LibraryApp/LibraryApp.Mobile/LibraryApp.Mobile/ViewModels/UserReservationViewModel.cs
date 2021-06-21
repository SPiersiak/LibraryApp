using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Reservations;
using LibraryApp.Mobile.ViewModels.DTO;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class UserReservationViewModel : BaseViewModel
    {
        public IReservationService _reservationService => DependencyService.Get<IReservationService>();
        public IBookService _bookService => DependencyService.Get<IBookService>();
        public ObservableCollection<ReservationBookDto> Reservations { get; }
        public Command LoadReservationCommand { get; }
        public Command HistoryReservationCommand { get; }
        public UserReservationViewModel()
        {
            Reservations = new ObservableCollection<ReservationBookDto>();
            LoadReservationCommand = new Command(async () => await ExecuteLoadItemsCommand());
            HistoryReservationCommand = new Command(MoveToHistory);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Reservations.Clear();
                var items = await _reservationService.GetActiveReservationForUser(long.Parse(Settings.UserId));
                //DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    var book = await _bookService.GetBookById(item.BookId);
                    var res = new ReservationBookDto()
                    {
                        ReservationId = item.ReservationId,
                        ReservationStart = item.ReservationStart,
                        ReservationEnd = item.ReservationEnd,
                        BookName = book.BookName
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
        private async void MoveToHistory()
        {
            await Shell.Current.GoToAsync(nameof(HistoryReservationPage));
        }
    }   
}
