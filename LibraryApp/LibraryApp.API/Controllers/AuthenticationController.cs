using LibraryApp.API.Models;
using LibraryApp.API.Repositories.LoginRegister;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginRegisterRepository _loginRegisterRepository;

        public AuthenticationController(ILoginRegisterRepository loginRegisterRepository)
        {
            _loginRegisterRepository = loginRegisterRepository;
        }
       
        [HttpPut]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string userName, string password)
        {
            if(String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return BadRequest();
            }
            var registerResult = await _loginRegisterRepository.RegisterAsync(firstName, lastName, email, userName, password);

            if(registerResult == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                var loginResult = await _loginRegisterRepository.LoginAsync(userName, password);

                if(loginResult == null)
                {
                    return BadRequest();
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
