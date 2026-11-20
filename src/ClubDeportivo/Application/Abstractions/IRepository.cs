using System.Linq.Expressions;
using ClubDeportivo.Domain.Common;

namespace ClubDeportivo.Application.Abstractions;

// ISP: contrato pequeno y generico. DIP: la capa Application depende de esta abstraccion, no de EF.
public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllWithAsync(string[] includes, CancellationToken ct = default);
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
}