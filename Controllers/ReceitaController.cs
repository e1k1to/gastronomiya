using System.Threading.Tasks;
using gastronomiya.Models;
using gastronomiya.Models.DTOs;
using gastronomiya.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReceitaById(int id)
        {
            ViewBag.Receita = await _receitas.GetById(id);
            return View();
        }

        [HttpGet("AddReceita")]
        public async Task<ActionResult> AddReceita()
        {
            return View(new ReceitaDTO());
        }

        [HttpPost("AddReceita")]
        public async Task<ActionResult> AddReceita([FromForm]ReceitaDTO receitaDTO)
        {
            if (receitaDTO == null)
            {
                return BadRequest("Dados incompletos...");
            }

            Receita receita = new Receita
            {
                Titulo = receitaDTO.Titulo,
                Ingredientes = receitaDTO.Ingredientes,
                ModoDePreparo = receitaDTO.ModoDePreparo
            };
            var _receita = await _receitas.Add(receita);
            return RedirectToAction("GetReceitaById", new { id = _receita.Id });
        }

    }
}
