using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Services;

public class MiembroService : IMiembroService
{
    private readonly IRepository<Miembro> _repo;

    public MiembroService(IRepository<Miembro> repo) => _repo = repo;

    public async Task<IReadOnlyList<MiembroDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Inscripciones" }, ct)).Select(ToDto).ToList();

    public async Task<MiembroDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<MiembroDto> CreateAsync(MiembroDto dto, CancellationToken ct = default)
    {
        var entity = new Miembro();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, MiembroDto dto, CancellationToken ct = default)
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

    private static void Apply(Miembro e, MiembroDto d)
    {
        e.Nombre = d.Nombre.Trim();
        e.Apellido = d.Apellido.Trim();
        e.Email = d.Email.Trim().ToLowerInvariant();
        e.Telefono = d.Telefono.Trim();
    }

    private static MiembroDto ToDto(Miembro x) => new()
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Apellido = x.Apellido,
        Email = x.Email,
        Telefono = x.Telefono,
        ClubesActivos = x.ClubesActivos,
    };
}