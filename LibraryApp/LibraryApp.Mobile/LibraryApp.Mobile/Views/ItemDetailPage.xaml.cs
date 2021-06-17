using LibraryApp.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}