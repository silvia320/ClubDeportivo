using ClubDeportivo.Application.Dtos;

namespace ClubDeportivo.Application.Abstractions;

public interface IClubService
{
    Task<IReadOnlyList<ClubDto>> GetAllAsync(CancellationToken ct = default);
    Task<ClubDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ClubDto> CreateAsync(ClubDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, ClubDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}