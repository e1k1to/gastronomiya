using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using gastronomiya.Models;
using gastronomiya.Models.Context;
using gastronomiya.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Controllers.Services;

public class UserService : IUserService
{
    private readonly AppDBContext _context;

    public UserService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<User> Add(User user)
    {
        ValidateUser(user);
        var salt = CreateSalt();

        var _user = new User
        {
            Username = user.Username,
            Password = HashPassword(user.Password, salt),
            Salt = salt
        };
        await _context.Users.AddAsync(_user);
        await _context.SaveChangesAsync();
        return _user;
    }

    public async Task Delete(int id)
    {
        var user = await GetById(id);

        if (user == null)
        {
            throw new ArgumentException("Usuário não existe.");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetByName(string name)
    {
        return await _context.Users.Where(u => u.Username.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task<User> Update(User user)
    {
        var userExistente = await GetById(user.Id);
        if (userExistente == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }

        ValidateUser(user);

        userExistente.Username = user.Username;
        userExistente.Password = HashPassword(user.Password, userExistente.Salt);
        await _context.SaveChangesAsync();
        return userExistente;
    }
    private void ValidateUser(User user)
    {
        var userWithSameName = _context.Users.FirstOrDefault(u => u.Username == user.Username);
        if (userWithSameName != null && userWithSameName.Id != user.Id)
        {
            throw new ArgumentException("Nome de usuário já em uso.");
        }

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ArgumentException("Nome de usuário não pode ser vazio");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentException("Senha não pode ser vazia.");
        }
        
        if(user.Password.Length < 8)
        {
            throw new ArgumentException("Senha deve ter mais 8 ou mais caracteres.");
        }
    }

    private string CreateSalt(int size = 32)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
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
