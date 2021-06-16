using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PentagroPGDI.Repository
{
    public interface IRepository<TEntity> : IDisposable  where TEntity : class
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(int id);
        void Remove(int id);
    }
}
