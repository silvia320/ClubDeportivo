using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Infrastructure.Persistence.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> b)
    {
        b.ToTable("clubes");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("club_id");
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(80).HasColumnName("nombre");
        b.HasIndex(x => x.Nombre).IsUnique();
        b.Property(x => x.Deporte).IsRequired().HasMaxLength(60).HasColumnName("deporte");
        b.Property(x => x.Ubicacion).IsRequired().HasMaxLength(120).HasColumnName("ubicacion");
        b.Property(x => x.FechaFundacion).HasColumnName("fecha_fundacion");
        b.Ignore(x => x.MiembrosActivos);   
    }
}