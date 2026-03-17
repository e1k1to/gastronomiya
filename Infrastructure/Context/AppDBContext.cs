using System;
using gastronomiya.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Infrastructure.Context;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Receita> Receitas { get; set; }
}
