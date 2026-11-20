using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Services;


public class ClubService : IClubService
{
    private readonly IRepository<Club> _repo;

    public ClubService(IRepository<Club> repo) => _repo = repo;

    public async Task<IReadOnlyList<ClubDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllWithAsync(new[] { "Inscripciones" }, ct)).Select(ToDto).ToList();

    public async Task<ClubDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ClubDto> CreateAsync(ClubDto dto, CancellationToken ct = default)
    {
        var entity = new Club();
        Apply(entity, dto);
        await _repo.AddAsync(entity, ct);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, ClubDto dto, CancellationToken ct = default)
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

    private static void Apply(Club e, ClubDto d)
    {
        e.Nombre = d.Nombre.Trim();
        e.Deporte = d.Deporte.Trim();
        e.Ubicacion = d.Ubicacion.Trim();
        e.FechaFundacion = d.FechaFundacion;
    }

    private static ClubDto ToDto(Club x) => new()
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Deporte = x.Deporte,
        Ubicacion = x.Ubicacion,
        FechaFundacion = x.FechaFundacion,
        MiembrosActivos = x.MiembrosActivos,
    };
}