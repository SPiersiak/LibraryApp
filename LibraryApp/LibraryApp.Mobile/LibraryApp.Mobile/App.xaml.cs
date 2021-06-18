using LibraryApp.Mobile.Services;
using LibraryApp.Mobile.Services.Authors;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Categories;
using LibraryApp.Mobile.Services.Publishers;
using LibraryApp.Mobile.Services.Request;
using LibraryApp.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IRequestService, RequestService>();
            DependencyService.Register<IBookService, BookService>();
            DependencyService.Register<IAuthorService, AuthorService>();
            DependencyService.Register<ICategoryService, CategoryService>();
            DependencyService.Register<IPublisherService, PublisherService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
