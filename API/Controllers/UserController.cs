using gastronomiya.Application.Receitas.Interfaces;
using gastronomiya.Application.Users.Interfaces;
using gastronomiya.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _users;

        public UserController(IUserService users)
        {
            _users = users;
        }

        // GET: UserController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarUsuario(
            [FromQuery] string username,
            [FromQuery] string password
        )
        {
            var creds = new UserAccessDto() { Username = username, Password = password };

            var newUser = await _users.Add(creds);

            return Ok(newUser);

        }
    }
}
