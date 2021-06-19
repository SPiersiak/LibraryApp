using LibraryApp.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPublisherPage : ContentPage
    {
        public NewPublisherPage()
        {
            InitializeComponent();
            BindingContext = new NewPublisherViewModel();
        }
    }
}