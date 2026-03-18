using gastronomiya.Application.Receitas.DTOs;
using gastronomiya.Domain.Entities;
using System;

namespace gastronomiya.Infrastructure.Interfaces;

public interface IReceitaRepository
{
    public Task<List<Receita>> GetAll();
    public Task<Receita?> GetById(int id);
    public Task<List<Receita>> GetByName(string name);
    public Task<Receita> Add(ReceitaAccessDto receita);
    public Task Delete(int id);
    public Task<Receita> Update(int id, ReceitaAccessDto receita);

}
