using Microsoft.EntityFrameworkCore;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Clubes.AnyAsync())
        {
            db.Clubes.AddRange(
                new Club { Nombre = "Club Deportivo Los Andes", Deporte = "Fútbol",   Ubicacion = "Quito, Parque La Carolina", FechaFundacion = new DateOnly(1998, 3, 15) },
                new Club { Nombre = "Club de Natación Pacífico", Deporte = "Natación", Ubicacion = "Guayaquil, Malecón 2000",   FechaFundacion = new DateOnly(2005, 8, 20) },
                new Club { Nombre = "Tenis Club Cuenca",         Deporte = "Tenis",    Ubicacion = "Cuenca, Av. Ordóñez Lasso",  FechaFundacion = new DateOnly(2012, 5, 10) });
        }

        if (!await db.Miembros.AnyAsync())
        {
            db.Miembros.AddRange(
                new Miembro { Nombre = "Ana",    Apellido = "Pérez",   Email = "ana.perez@correo.com",    Telefono = "0991234567" },
                new Miembro { Nombre = "Luis",   Apellido = "Andrade", Email = "luis.andrade@correo.com", Telefono = "0987654321" },
                new Miembro { Nombre = "María",  Apellido = "Torres",  Email = "maria.torres@correo.com", Telefono = "0976543210" },
                new Miembro { Nombre = "Carlos", Apellido = "Mena",    Email = "carlos.mena@correo.com",  Telefono = "0965432109" });
        }

        await db.SaveChangesAsync();

        if (!await db.Inscripciones.AnyAsync())
        {
            var clubes   = await db.Clubes.OrderBy(c => c.Id).ToListAsync();
            var miembros = await db.Miembros.OrderBy(m => m.Id).ToListAsync();

            if (clubes.Count >= 3 && miembros.Count >= 4)
            {
                db.Inscripciones.AddRange(
                    new Inscripcion { Club = clubes[0], Miembro = miembros[0], FechaInscripcion = new DateOnly(2025, 2, 10), Estado = "A" },
                    new Inscripcion { Club = clubes[0], Miembro = miembros[1], FechaInscripcion = new DateOnly(2025, 3, 1),  Estado = "A" },
                    new Inscripcion { Club = clubes[1], Miembro = miembros[0], FechaInscripcion = new DateOnly(2025, 6, 18), Estado = "S" },
                    new Inscripcion { Club = clubes[1], Miembro = miembros[2], FechaInscripcion = new DateOnly(2025, 9, 5),  Estado = "A" },
                    new Inscripcion { Club = clubes[2], Miembro = miembros[3], FechaInscripcion = new DateOnly(2026, 1, 12), Estado = "I" });
                await db.SaveChangesAsync();
            }
        }
    }
}