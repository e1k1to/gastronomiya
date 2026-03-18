using gastronomiya.Application.Auth;
using gastronomiya.Application.Auth.DTOs;
using gastronomiya.Application.Receitas.DTOs;
using gastronomiya.Application.Receitas.Interfaces;
using gastronomiya.Application.Users.Interfaces;
using gastronomiya.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            var creds = new UserAccessDto() { Username = loginDto.Username, Password = loginDto.Password };

            var token = await _auth.Login(creds);

            if(token == String.Empty)
            {
                return Unauthorized();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            if (Request.Headers["Referer"].ToString().Contains("/swagger/index.html"))
            {
                return Ok(new
                {
                    token,
                    tokenType = "Bearer",
                    expiresIn = 86400,
                    user = new { username = loginDto.Username }
                });
            }

            Response.Cookies.Append("jwt_token", token, cookieOptions);

            return RedirectToAction("Index", "Home");
        }
    }
}
