using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private bool userAcces;
        private bool adminAcces;
        public AppViewModel()
        {
            if(Settings.Role == "Tak")
            {
                UserAcces = true;
                AdminAcces = true;
            }
            else if(Settings.Role == "Nie")
            {
                UserAcces = true;
                AdminAcces = false;
            }
            else if(Settings.Role == "Anonnymous")
            {
                AdminAcces = false;
                UserAcces = false;
            }
        }
        public bool UserAcces { get => userAcces; set => SetProperty(ref userAcces, value); }
        public bool AdminAcces { get => adminAcces; set => SetProperty(ref adminAcces, value); }
    }
}
