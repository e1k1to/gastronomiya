using System;
using Microsoft.EntityFrameworkCore;

namespace gastronomiya.Models.Context;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Receita> Receitas { get; set; }
}
