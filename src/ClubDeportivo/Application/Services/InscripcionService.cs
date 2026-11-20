using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Services;

public class InscripcionService : IInscripcionService
{
    private readonly IRepository<Inscripcion> _repo;

    public InscripcionService(IRepository<Inscripcion> repo) => _repo = repo;

    public async Task<IReadOnlyList<InscripcionDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Club", "Miembro" }, ct)).Select(ToDto).ToList();

    public async Task<InscripcionDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<InscripcionDto> CreateAsync(InscripcionDto dto, CancellationToken ct = default)
    {
        var entity = new Inscripcion();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, InscripcionDto dto, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        Apply(entity, dto);
        await _repo.UpdateAsync(entity, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        return true;
    }

    private static void Apply(Inscripcion e, InscripcionDto d)
    {
        e.ClubId = d.ClubId;
        e.MiembroId = d.MiembroId;
        e.FechaInscripcion = d.FechaInscripcion;
        e.Estado = d.Estado;
    }

    private static InscripcionDto ToDto(Inscripcion x) => new()
    {
        Id = x.Id,
        ClubId = x.ClubId,
        ClubTexto = x.Club is null ? null : x.Club.Nombre,
        MiembroId = x.MiembroId,
        MiembroTexto = x.Miembro is null ? null : string.Join(" ", x.Miembro.Nombre, x.Miembro.Apellido),
        FechaInscripcion = x.FechaInscripcion,
        Estado = x.Estado,
    };
}