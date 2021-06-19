using LibraryApp.Mobile.ViewModels;
using LibraryApp.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LibraryApp.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddingDataPage), typeof(AddingDataPage));
        }

    }
}
