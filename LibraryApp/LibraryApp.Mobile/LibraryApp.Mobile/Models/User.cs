using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Mobile.Models
{
    public class User
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmployee { get; set; }
    }
}
