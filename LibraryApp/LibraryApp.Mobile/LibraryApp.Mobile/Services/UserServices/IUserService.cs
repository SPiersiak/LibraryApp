using LibraryApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> UpdateUserData(User user);
    }
}
