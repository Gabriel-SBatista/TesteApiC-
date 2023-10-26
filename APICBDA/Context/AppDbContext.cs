using APICBDA.Models;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Prova> Provas { get; set; }
    public DbSet<Sexo> Sexos { get; set; }
    public DbSet<Estilo> Estilos { get; set; }
}
