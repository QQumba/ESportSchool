using ESportSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESportSchool.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity e);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> PageAsync(int skip, int take);
        Task UpdateAsync(TEntity e);
        void Remove(TEntity e);
    }
}
