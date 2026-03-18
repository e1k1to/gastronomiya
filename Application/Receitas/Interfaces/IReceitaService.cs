using gastronomiya.Application.Receitas.DTOs;
using gastronomiya.Domain.Entities;
using System;

namespace gastronomiya.Application.Receitas.Interfaces;

public interface IReceitaService
{
    Task<List<Receita>> GetAll();
    Task <Receita?> GetById(int id);
    Task<List<Receita>> GetByName(string name);
    Task<Receita> Add(ReceitaAccessDto receita);
    Task Delete(int id);
    Task<Receita> Update(int id, ReceitaAccessDto receita);

}
