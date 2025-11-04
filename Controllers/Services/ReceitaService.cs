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

    public async Task<Receita> Add(Receita receita)
    {
        ValidateReceita(receita);

        var _receita = new Receita
        {
            Titulo = receita.Titulo,
            Ingredientes = receita.Ingredientes,
            ModoDePreparo = receita.ModoDePreparo
        };

        await _context.Receitas.AddAsync(_receita);
        await _context.SaveChangesAsync();
        return _receita;

    }

    public async Task Delete(int id)
    {
        var receita = await GetById(id);
        if (receita == null)
        {
            throw new ArgumentException("Receita não existe.");
        }
        _context.Receitas.Remove(receita);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Receita>> GetAll()
    {
        var todasReceitas = await _context.Receitas.ToListAsync();
        return todasReceitas;
    }

    public async Task<Receita?> GetById(int id)
    {
        return await _context.Receitas.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<Receita>> GetByName(string name)
    {
        return await _context.Receitas.Where(u => u.Titulo.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task<Receita> Update(Receita receita)
    {
        var receitaExistente = await GetById(receita.Id);
        if (receitaExistente == null)
        {
            throw new ArgumentException("Receita não encontrada.");
        }

        ValidateReceita(receita);

        receitaExistente.Titulo = receita.Titulo;
        receitaExistente.Ingredientes = receita.Ingredientes;
        receitaExistente.ModoDePreparo = receita.ModoDePreparo;

        await _context.SaveChangesAsync();
        return receitaExistente;
    }
    
    private void ValidateReceita(Receita receita)
    {
        var receitaWithSameTitle =  _context.Receitas.FirstOrDefault(u => u.Titulo == receita.Titulo);
        if (receitaWithSameTitle != null && receitaWithSameTitle.Id != receita.Id)
        {
            throw new ArgumentException("Titulo já em uso.");
        }

        if (string.IsNullOrEmpty(receita.Titulo))
        {
            throw new ArgumentException("Titulo não pode ser vazio.");
        }

        if (string.IsNullOrEmpty(receita.Ingredientes))
        {
            throw new ArgumentException("Campo 'Ingredentes' não pode ser vazio.");
        }

        if (string.IsNullOrEmpty(receita.ModoDePreparo))
        {
            throw new ArgumentException("Campo 'Modo de preparo' não pode ser vazio.");
        }

    }
}
