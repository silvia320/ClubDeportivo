# ClubDeportivo

Sistema de Gestión de Clubs Deportivos: Diseña un sistema usando .net o php para gestionar clubes deportivos y miembros, incluyendo registros. Tablas: `Clubes` (club_id, nombre, deporte, ubicacion, fecha_fundacion), `Miembros` (miembro_id, nombre, apellido, email, telefono).

Monolito MVC (.NET 10) con EF Core + PostgreSQL + FluentValidation. Capas dentro de un solo proyecto: Domain, Application (abstracciones, servicios, validadores), Infrastructure y Web (Controllers/Views).

## Modelo
- **clubes** (club_id, nombre, deporte, ubicacion, fecha_fundacion)
- **miembros** (miembro_id, nombre, apellido, email, telefono)
- **inscripciones** (inscripcion_id, club_id, miembro_id, fecha_inscripcion, estado): un miembro puede estar en varios clubes, pero una sola vez en cada uno. Estado: A = Activa, S = Suspendida, I = Inactiva.

El diagrama está en `docs/modelo.puml` (PlantUML).

## Migraciones nuevas
    .\scripts\migrations.ps1 -Name NombreMigracion