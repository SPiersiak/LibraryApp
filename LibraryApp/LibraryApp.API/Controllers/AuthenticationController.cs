using LibraryApp.API.Models;
using LibraryApp.API.Repositories.LoginRegister;
using LibraryApp.API.Repositories.LoginRegister.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    /// <summary>
    /// Controler dajacy dostęp zewnetrzym aplikacjom do bazy danych, służy do autoryzacji uzytkownika(logowanie, rejestracja)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginRegisterRepository _loginRegisterRepository;
        /// <summary>
        /// konstruktor inicjalizujący Dependency Incjection do repozytorium
        /// </summary>
        /// <param name="loginRegisterRepository">interfejst do repozytorium opisujacy operacja na bazie danych</param>
        public AuthenticationController(ILoginRegisterRepository loginRegisterRepository)
        {
            _loginRegisterRepository = loginRegisterRepository;
        }

       /// <summary>
       /// metoda dodajacego nowego uzytkownika do bazy danych, obsluguje rejestracje w aplikacji
       /// </summary>
       /// <param name="registerDto">jak parametr przyjmuje imie, nazwisko, email, hasło, username</param>
       /// <returns>zwraca ok jezeli uzytkownik zostła stworzony</returns>
        [HttpPut]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if(String.IsNullOrEmpty(registerDto.FirstName) || String.IsNullOrEmpty(registerDto.LastName) || String.IsNullOrEmpty(registerDto.Email) || String.IsNullOrEmpty(registerDto.UserName) || String.IsNullOrEmpty(registerDto.Password))
            {
                return BadRequest();
            }
            var registerResult = await _loginRegisterRepository.RegisterAsync(registerDto.FirstName, registerDto.LastName, registerDto.Email, registerDto.UserName, registerDto.Password);

            if(registerResult == false)
            {
                return Ok("istnieje juz taki uzytkowik");
                //BadRequest("istnieje juz taki uzytkowik");
            }
            return Ok("utworzono nowego uzytkownika");
        }
        /// <summary>
        /// metoda obsługujaca logowanie do aplikacji, sprwdza czy podany login i haslo sa poprawne
        /// </summary>
        /// <param name="loginDto">jako parametr przyjmuje login i haslo podane przez uzytkownika i sprawdza ich poprawność</param>
        /// <returns>zwraca ok z modelem nowego uzytkownika jezeli dane byly prawidlowe</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResult = await _loginRegisterRepository.LoginAsync(loginDto.UserName, loginDto.Password);

                if(loginResult.UserName == null)
                {
                    return Ok(new User()
                    {
                        UserID = loginResult.UserID,
                        UserName = loginResult.UserName
                    });
                }
                return Ok(new User()
                {
                   UserID = loginResult.UserID,
                   FirstName = loginResult.FirstName,
                   LastName = loginResult.LastName,
                   Email = loginResult.Email,
                   Password = loginResult.Password,
                   UserName = loginResult.UserName,
                   IsEmployee = loginResult.IsEmployee
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
    }
}
