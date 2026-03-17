using gastronomiya.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace gastronomiya.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _settings;
        private readonly IAuthRepository _auth;

        public AuthService(JwtSettings settings, IAuthRepository auth)
        {
            _settings = settings;
            _auth = auth;
        }

        public async Task<String> Login(string nome, string password)
        {
            var hashedPassword = HashPassword(password, nome);

            var user = await _auth.Login(nome, hashedPassword);
            if (user == null)
            {
                return String.Empty;
            }

            var token = GerarToken(user.Id, user.Username);

            return token;
        }

        private string GerarToken(int userId, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);

                byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
