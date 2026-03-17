using gastronomiya.Domain.Entities;
using System;

namespace gastronomiya.Application.Users.Interfaces;

public interface IUserService
{
    public Task<User?> GetById(int id);
    public Task<List<User>> GetByName(string name);
    public Task<User> Add(UserAccessDto user);
    public Task Delete(int id);
    public Task<User> Update(int id, UserAccessDto user);
}
