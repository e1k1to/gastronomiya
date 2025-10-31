using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gastronomiya.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(128)]
    public string Username { get; set; } = default!;

    [Required]
    [StringLength(128)]
    public string Password { get; set; } = default!;

    [Required]
    [StringLength(64)]
    public string Salt { get; set; } = default!;
}
