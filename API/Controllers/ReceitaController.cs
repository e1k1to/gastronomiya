using gastronomiya.Application.Receitas.DTOs;
using gastronomiya.Application.Receitas.Interfaces;
using gastronomiya.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace gastronomiya.API.Controllers
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
            ViewBag.ReceitaId = id;
            return View();
        }

        [Authorize]
        [HttpGet("Adicionar")]
        public async Task<ActionResult> AddReceita()
        {
            return View(new ReceitaDTO());
        }

        [Authorize]
        [HttpPost("Adicionar")]
        public async Task<ActionResult> AddReceita([FromForm] ReceitaDTO receitaDTO)
        {
            if (receitaDTO == null)
            {
                return BadRequest("Dados incompletos...");
            }

            ReceitaAccessDto receita = new ReceitaAccessDto
            {
                Titulo = receitaDTO.Titulo,
                Ingredientes = receitaDTO.Ingredientes,
                ModoDePreparo = receitaDTO.ModoDePreparo
            };
            var _receita = await _receitas.Add(receita);
            return RedirectToAction("GetReceitaById", new { id = _receita.Id });
        }

        [Authorize]
        [HttpGet("Editar/{id}")]
        public async Task<ActionResult> EditarReceita([FromRoute] int id)
        {
            var receita = await _receitas.GetById(id);
            var receitaDto = new ReceitaDTO() { Ingredientes = receita.Ingredientes, ModoDePreparo = receita.ModoDePreparo, Titulo = receita.Titulo };
            ViewBag.ReceitaId = id;
            return View(receitaDto);
        }

        [Authorize]
        //Razor não consegue usar PUT o,...,o
        [HttpPost("Editar/{id}")]
        public async Task<ActionResult> EditarReceita(int id, [FromForm] ReceitaDTO receitaDTO)
        {
            if(receitaDTO == null)
            {
                return BadRequest("Receita não existe...");
            }

            ReceitaAccessDto receita = new ReceitaAccessDto
            {
                Titulo = receitaDTO.Titulo,
                Ingredientes = receitaDTO.Ingredientes,
                ModoDePreparo = receitaDTO.ModoDePreparo
            };

            var _receita = await _receitas.Update(id, receita);
            return RedirectToAction("GetReceitaById", new { id = _receita.Id });
        }
    }
}
