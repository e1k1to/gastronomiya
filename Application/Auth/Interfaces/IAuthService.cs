using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using gastronomiya.Domain.Entities;

namespace gastronomiya.Application.Auth
{
    public interface IAuthService
    {
        public Task<String> Login(string nome, string password);
    }
}
