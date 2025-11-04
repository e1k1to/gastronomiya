using System.Threading.Tasks;
using gastronomiya.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly IReceitaService _receitas;

        public ReceitaController(IReceitaService receitas)
        {
            _receitas = receitas;
        }


        // GET: ReceitaController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewBag.TodasReceitas = await _receitas.GetAll();
            return View();
        }

    }
}
