using System;
namespace gastronomiya.Models.DTOs;

public class ReceitaDTO
{
    public int Id { get; set; } = default!;
    public string Titulo { get; set; }
    public string Ingredientes { get; set; }
    public string ModoDePreparo { get; set; }
}
