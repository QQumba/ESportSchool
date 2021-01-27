using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.NotMapped;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ESportSchoolDBContext _db;
        private DbSet<TEntity> _set;

        protected Repository(ESportSchoolDBContext context)
        {
            _db = context;
        }

        protected DbSet<TEntity> Set => _set ??= _db.Set<TEntity>();

        protected async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        protected void SaveChanges()
        {
            _db.SaveChanges();
        }

        public async Task AddAsync(TEntity e)
        {
            e.CreationTimestamp = DateTime.Now;
            e.LastUpdateTimestamp = e.CreationTimestamp;
            await Set.AddAsync(e);
            await SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Set.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Set.Select(e => e).ToListAsync();
        }

        public async Task<List<TEntity>> PageAsync(int skip, int take)
        {
            return await Set.Skip(skip).Take(take).ToListAsync();
        }

        public async Task UpdateAsync(TEntity e)
        {
            e.LastUpdateTimestamp = DateTime.Now;
            Set.Update(e);
            await SaveChangesAsync();
        }

        public void Remove(TEntity e)
        {
            Set.Remove(e);
            SaveChanges();
        }
    }
}
