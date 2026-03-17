using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using gastronomiya.Domain.Entities;
using gastronomiya.Infrastructure.Interfaces;
using gastronomiya.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using gastronomiya.Application.Exceptions;

namespace gastronomiya.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDBContext _context;

    public UserRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<User> Add(UserAccessDto user)
    {
        ValidateUser(user);

        var _user = new User
        {
            Username = user.Username,
            Password = HashPassword(user.Password, user.Username),
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
        return await _context.Users.Where(u => u.Username.ToLower().Contains(name)).ToListAsync();
    }

    public async Task<User> Update(int id, UserAccessDto user)
    {
        var userExistente = await GetById(id);
        if (userExistente == null)
        {
            throw new UserNotFound("Usuário não encontrado.");
        }

        ValidateUser(user);

        userExistente.Username = user.Username;
        userExistente.Password = HashPassword(user.Password, user.Username);
        await _context.SaveChangesAsync();
        return userExistente;
    }
    private void ValidateUser(UserAccessDto user)
    {
        var userWithSameName = _context.Users.FirstOrDefault(u => u.Username == user.Username);
        if (userWithSameName != null)
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
