using System;

namespace gastronomiya.Models.Interfaces;

public interface IUserService
{
    Task<User?> GetById(int id);
    Task<List<User>> GetByName(string name);
    Task<User> Add(User user);
    Task Delete(int id);
    Task<User> Update(User user);
}
