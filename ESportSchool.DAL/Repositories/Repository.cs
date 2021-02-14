using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ESportSchool.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ESportSchoolDbContext _db;
        private DbSet<TEntity> _set;

        protected Repository(ESportSchoolDbContext context)
        {
            _db = context;
        }

        protected DbSet<TEntity> Set => _set ??= _db.Set<TEntity>();

        public async Task CreateAsync(TEntity e, CancellationToken ct = default)
        {
            e.CreationTimestamp = DateTime.Now;
            e.LastUpdateTimestamp = e.CreationTimestamp;
            await Set.AddAsync(e, ct);
            await SaveChangesAsync(ct);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default)
        {
            await Set.AddRangeAsync(e, ct);
            await SaveChangesAsync(ct);
        }

        public Task<TEntity> GetAsync(int id, CancellationToken ct = default)
        {
            return Set.FirstOrDefaultAsync(e => e.Id == id, cancellationToken: ct);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return Set.Select(e => e).ToListAsync(cancellationToken: ct);
        }

        public Task<List<TEntity>> PageAsync(int skip, int take, CancellationToken ct = default)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken: ct);
        }

        public async Task UpdateAsync(TEntity e, CancellationToken ct = default)
        {
            e.LastUpdateTimestamp = DateTime.Now;
            Set.Update(e);
            await SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = Set.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Set.Remove(entity);
                await SaveChangesAsync(ct);
            }
        }

        public async Task DeleteAsync(TEntity e, CancellationToken ct = default)
        {
            Set.Remove(e);
            await SaveChangesAsync(ct);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default)
        {
            Set.RemoveRange(e);
            await SaveChangesAsync(ct);
        }
        
        protected async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _db.SaveChangesAsync(ct);
        }

        protected void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
