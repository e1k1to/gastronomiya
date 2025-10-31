using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gastronomiya.Models;

public class Receita
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(128)]
    public string Titulo { get; set; } = default!;

    [Required]
    [StringLength(256)]
    public string Ingredientes { get; set; } = default!;

    [Required]
    [StringLength(8192)]
    public string ModoDePreparo { get; set; } = default!;

}
