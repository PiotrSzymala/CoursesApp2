using CoursesApp.Models.DTOs;
using CoursesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApp.Controllers.API
{
    [Route("api/account")]
    [ApiController]
    public class AccountController2DD : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController2DD(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] UserLoginDto dto)
        {
            if (_accountService.AuthenticateUser(dto.Login, dto.Password))
            {
                // Tutaj możesz zwrócić token JWT lub inne dane użytkownika
                return Ok("Authentication successful");
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
