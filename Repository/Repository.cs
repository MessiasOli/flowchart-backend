using System.Collections.Generic;
using System.Linq;

namespace Agricultural_Plan
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private NODEContext context = null;
        public Repository(NODEContext context)
        {
            this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetByQuery()
        {
            return context.Set<TEntity>().AsQueryable();
        }

        public TEntity GetById(int id)
        {
            try { return context.Set<TEntity>().Find(id); }
            catch { return null; }
        }

        public TEntity GetById(string id)
        {
            try { return context.Set<TEntity>().Find(id); }
            catch { return null; }
        }

        public TEntity GetById(string idarea, string idmaterial)
        {
            try { return context.Set<TEntity>().Find(idarea, idmaterial); }
            catch { return null; }
        }
    }
}
