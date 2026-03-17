 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gastronomiya.Domain.Entities;

public class UserAccessDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
