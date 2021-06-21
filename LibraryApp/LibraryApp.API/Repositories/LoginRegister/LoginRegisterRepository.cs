using LibraryApp.API.Data;
using LibraryApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.LoginRegister
{
    /// <summary>
    /// klasa obslugujaca logoawnie i rejestracje
    /// </summary>
    public class LoginRegisterRepository : ILoginRegisterRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public LoginRegisterRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        /// <summary>
        /// metoda sprawdzajaca czy dane podane podczas logowanie sa poprawne
        /// </summary>
        /// <param name="userName">zmiena ciagu znaków zawierajaca login</param>
        /// <param name="password">zmienna ciagu znakow zawierajaca hasło</param>
        /// <returns>zwraca usera z danym jezeli dane były poprawne</returns>
        public async Task<User> LoginAsync(string userName, string password)
        {
            var user = await _libraryDbContext.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();
            if(user == null)
            {
                return new User()
                {
                    UserID = 0,
                    UserName = null
                };
            }
            if(user.Password != password)
            {
                return new User()
                {
                    UserID = 0,
                    UserName = null
                };
            }
            return new User()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                IsEmployee = user.IsEmployee
            };
        }
        /// <summary>
        /// metoda służaca do rejestracji uzytkownika czyli dodania go do bazy danych
        /// </summary>
        /// <param name="firstName">imie nowego uzytkownika</param>
        /// <param name="lastName">nawisko nowego uzytkownika  </param>
        /// <param name="email">email nowego uzytkownika</param>
        /// <param name="userName">login nowego uzytkownika</param>
        /// <param name="password">haslo nowego uzytkownika</param>
        /// <returns>zwraca true jezeli dodanie nowego uzytkownika sie powiodło, jezli nie false</returns>
        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string userName, string password)
        {
            var user = await _libraryDbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if(user != null)
            {
                return false;
                //throw new Exception("Jest juz taki user");
            }
            if(String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                return false;
                //throw new Exception("Wymagane pola nie są uzupełnine");
            }
            var newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password,
                IsEmployee = false
            };

            await _libraryDbContext.Users.AddAsync(newUser);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
