using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Infrastructure.Persistence.Configurations;

public class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
{
    public void Configure(EntityTypeBuilder<Inscripcion> b)
    {
        b.ToTable("inscripciones");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("inscripcion_id");
        b.Property(x => x.ClubId).HasColumnName("club_id");
        b.HasOne(x => x.Club)
            .WithMany(t => t.Inscripciones)
            .HasForeignKey(x => x.ClubId)
            .OnDelete(DeleteBehavior.Restrict);
        b.Property(x => x.MiembroId).HasColumnName("miembro_id");
        b.HasOne(x => x.Miembro)
            .WithMany(t => t.Inscripciones)
            .HasForeignKey(x => x.MiembroId)
            .OnDelete(DeleteBehavior.Restrict);
        b.Property(x => x.FechaInscripcion).HasColumnName("fecha_inscripcion");
        b.Property(x => x.Estado).IsRequired().HasMaxLength(1).HasColumnName("estado");
        b.HasIndex(x => new { x.ClubId, x.MiembroId }).IsUnique();
    }
}