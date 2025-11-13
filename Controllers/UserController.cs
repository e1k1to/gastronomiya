using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        // GET: UserController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
