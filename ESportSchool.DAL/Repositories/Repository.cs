using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAsync(TEntity e)
        {
            e.CreationTimestamp = DateTime.Now;
            e.LastUpdateTimestamp = e.CreationTimestamp;
            await Set.AddAsync(e);
        }

        public async Task<TEntity> GetAsync(int id)
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

        public void Update(TEntity e)
        {
            e.LastUpdateTimestamp = DateTime.Now;
            Set.Update(e);
        }

        public void Delete(int id)
        {
            var entity = Set.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Set.Remove(entity);
            }
        }

        public void Delete(TEntity e)
        {
            Set.Remove(e);
        }

        public void DeleteRange(IEnumerable<TEntity> e)
        {
            Set.RemoveRange(e);
        }
        
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        protected void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
