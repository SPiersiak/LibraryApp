using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.ViewModels;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp.Mobile.Views
{
    public partial class ItemsPage : ContentPage
    {
        public IBookService _bookService => DependencyService.Get<IBookService>();
        ItemsViewModel _viewModel;
        IEnumerable<Book> search;

        public ItemsPage()
        {
            InitializeComponent();
            Generate();
            BindingContext = _viewModel = new ItemsViewModel();
        }
        private async void Generate()
        {
            search = await _bookService.GetBooks();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = search.Where(x => x.BookName.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            ItemsListView.ItemsSource = list;
        }
    }
}