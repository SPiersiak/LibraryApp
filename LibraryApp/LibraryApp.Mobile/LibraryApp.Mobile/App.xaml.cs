using LibraryApp.Mobile.Services;
using LibraryApp.Mobile.Services.Authentication;
using LibraryApp.Mobile.Services.Authors;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Categories;
using LibraryApp.Mobile.Services.Publishers;
using LibraryApp.Mobile.Services.Request;
using LibraryApp.Mobile.Services.UserServices;
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
            DependencyService.Register<IAuthenticationService, AuthenticationService>();
            DependencyService.Register<IUserService, UserService>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
