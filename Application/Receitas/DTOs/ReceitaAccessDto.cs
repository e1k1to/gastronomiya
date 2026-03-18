using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.Application.Receitas.DTOs;

public class ReceitaAccessDto
{
    public string Titulo { get; set; } = default!;
    public string Ingredientes { get; set; } = default!;
    public string ModoDePreparo { get; set; } = default!;

}
