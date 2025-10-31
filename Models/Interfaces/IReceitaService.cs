using System;

namespace gastronomiya.Models.Interfaces;

public interface IReceitaService
{
    Task <Receita?> GetById(int id);
    Task<List<Receita>> GetByName(string name);
    Task<Receita> Add(Receita receita);
    Task Delete(int id);
    Task<Receita> Update(Receita receita);

}
