using ClubDeportivo.Application.Dtos;

namespace ClubDeportivo.Application.Abstractions;

public interface IInscripcionService
{
    Task<IReadOnlyList<InscripcionDto>> GetAllAsync(CancellationToken ct = default);
    Task<InscripcionDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<InscripcionDto> CreateAsync(InscripcionDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, InscripcionDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}