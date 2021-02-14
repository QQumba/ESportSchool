using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESportSchool.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity e, CancellationToken ct = default);
        public Task CreateRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default);
        Task<TEntity> GetAsync(int id, CancellationToken ct = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<List<TEntity>> PageAsync(int skip, int take, CancellationToken ct = default);
        Task UpdateAsync(TEntity e, CancellationToken ct = default);
        Task DeleteAsync(TEntity e, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task DeleteRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default);
    }
}
