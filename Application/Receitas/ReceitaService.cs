using System;
using System.Security.Cryptography;
using System.Text;
using gastronomiya.Application.Receitas.Interfaces;
using gastronomiya.Domain.Entities;
using gastronomiya.Infrastructure.Context;
using gastronomiya.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Application.Receitas;

public class ReceitaService : IReceitaService
{

    private readonly IReceitaRepository _receita;

    public ReceitaService(IReceitaRepository receita)
    {
        _receita = receita;
    }

    public async Task<Receita> Add(ReceitaAccessDto receita)
    {
        return await (_receita.Add(receita));
    }

    public async Task Delete(int id)
    {
        await _receita.Delete(id);
        return;
    }

    public async Task<List<Receita>> GetAll()
    {
        return await _receita.GetAll();
    }

    public async Task<Receita?> GetById(int id)
    {
        return await _receita.GetById(id);
    }

    public async Task<List<Receita>> GetByName(string name)
    {
        return await _receita.GetByName(name.ToLower());
    }

    public async Task<Receita> Update(int id, ReceitaAccessDto receita)
    {
        return await _receita.Update(id, receita);
    }
    
}
