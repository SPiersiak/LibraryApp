using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Book _selectedItem;
        public IBookService _bookService => DependencyService.Get<IBookService>();
        public ObservableCollection<Book> Books { get; }

        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<Book> BookTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Books = new ObservableCollection<Book>();
            LoadBooksCommand = new Command(async () => await ExecuteLoadItemsCommand());

            BookTapped = new Command<Book>(OnItemSelected);

            AddBookCommand = new Command(OnAddItem);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Books.Clear();
                var items = await _bookService.GetBooks();
                    //DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Books.Add(item);
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
            SelectedItem = null;
        }

        public Book SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Book item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.BookId)}={item.BookId}");
        }
    }
}