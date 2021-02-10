using ESportSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESportSchool.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity e);
        Task<TEntity> GetAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> PageAsync(int skip, int take);
        void Update(TEntity e);
        void Delete(TEntity e);
        void Delete(int id);
        void DeleteRange(IEnumerable<TEntity> e);
        Task SaveChangesAsync();
    }
}
