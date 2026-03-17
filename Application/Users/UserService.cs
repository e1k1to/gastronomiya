using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using gastronomiya.Application.Users.Interfaces;
using gastronomiya.Domain.Entities;
using gastronomiya.Infrastructure.Context;
using gastronomiya.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _user;

    public UserService(IUserRepository user)
    {
        _user = user;
    }

    public async Task<User> Add(UserAccessDto user)
    {
        return await _user.Add(user);
    }

    public async Task Delete(int id)
    {
        await _user.Delete(id);
        return;
    }

    public async Task<User?> GetById(int id)
    {
        return await _user.GetById(id);
    }

    public async Task<List<User>> GetByName(string name)
    {
        return await _user.GetByName(name.ToLower());
    }

    public async Task<User> Update(int id, UserAccessDto user)
    {
        
        return await _user.Update(id, user);
    }
}
