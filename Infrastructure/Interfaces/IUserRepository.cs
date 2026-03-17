using gastronomiya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace gastronomiya.Infrastructure.Interfaces;

public interface IUserRepository
{
    public Task<User> Add(UserAccessDto user);
    public Task Delete(int id);
    public Task<User?> GetById(int id);
    public Task<List<User>> GetByName(string name);
    public Task<User> Update(int id, UserAccessDto user);
}
