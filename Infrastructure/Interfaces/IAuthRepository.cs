using gastronomiya.Domain.Entities;
using gastronomiya.Infrastructure.Context;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace gastronomiya.Application.Auth
{
    public interface IAuthRepository
    {
        public Task<User?> Login(string nome, string hashedPassword);
    }
}
