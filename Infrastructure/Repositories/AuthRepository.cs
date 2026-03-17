using gastronomiya.Application.Auth;
using gastronomiya.Domain.Entities;
using gastronomiya.Infrastructure.Context;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace gastronomiya.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly JwtSettings _settings;
    private readonly AppDBContext _context;

    public AuthRepository(JwtSettings settings, AppDBContext context)
    {
        _settings = settings;
        _context = context;
    }

    public async Task<User?> Login(string nome, string hashedPassword)
    {
        return await _context.Users.Where(l => l.Username == nome && l.Password == hashedPassword).FirstOrDefaultAsync();
    }
}
