using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Infrastructure.Persistence.Configurations;

public class MiembroConfiguration : IEntityTypeConfiguration<Miembro>
{
    public void Configure(EntityTypeBuilder<Miembro> b)
    {
        b.ToTable("miembros");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("miembro_id");
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(60).HasColumnName("nombre");
        b.Property(x => x.Apellido).IsRequired().HasMaxLength(60).HasColumnName("apellido");
        b.Property(x => x.Email).IsRequired().HasMaxLength(120).HasColumnName("email");
        b.HasIndex(x => x.Email).IsUnique();
        b.Property(x => x.Telefono).IsRequired().HasMaxLength(15).HasColumnName("telefono");
        b.Ignore(x => x.ClubesActivos);   // calculado: no existe como columna
    }
}