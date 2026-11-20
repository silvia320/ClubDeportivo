using ClubDeportivo.Application.Dtos;

namespace ClubDeportivo.Application.Abstractions;

public interface IMiembroService
{
    Task<IReadOnlyList<MiembroDto>> GetAllAsync(CancellationToken ct = default);
    Task<MiembroDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<MiembroDto> CreateAsync(MiembroDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, MiembroDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}