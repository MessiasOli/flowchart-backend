using System.Collections.Generic;
using System.Linq;

namespace Agricultural_Plan
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetByQuery();
        TEntity GetById(int id);
        TEntity GetById(string id);
        TEntity GetById(string idarea, string idmaterial);
    }
}
