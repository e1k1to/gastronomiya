using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace gastronomiya.Application.Auth.DTOs;

public class LoginDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
