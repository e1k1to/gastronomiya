using System;
using System.Security.Cryptography;
using System.Text;
using gastronomiya.Models;
using gastronomiya.Models.Context;
using gastronomiya.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Controllers.Services;

public class ReceitaService : IReceitaService
{

    private readonly AppDBContext _context;

    public ReceitaService(AppDBContext context)
    {
        _context = context;
    }

    public Task<Receita> Add(Receita receita)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Receita?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Receita>> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Receita> Update(Receita receita)
    {
        throw new NotImplementedException();
    }

}
