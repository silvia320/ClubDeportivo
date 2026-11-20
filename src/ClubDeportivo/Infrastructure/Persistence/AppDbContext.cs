using Microsoft.EntityFrameworkCore;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Club> Clubes => Set<Club>();
    public DbSet<Miembro> Miembros => Set<Miembro>();
    public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}