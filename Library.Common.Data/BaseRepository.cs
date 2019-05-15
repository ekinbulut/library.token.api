using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Common.Data
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Dbcontext;

        protected BaseRepository(DbContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public void Delete(TEntity entity)
        {
            Dbcontext.Remove(entity);
        }

        public ICollection<TEntity> GetAll()
        {
            return Dbcontext.Query<TEntity>().ToList();
        }

        public TEntity GetOne(int id)
        {
            return Dbcontext.Find<TEntity>(id);
        }

        public TEntity Insert(TEntity entity)
        {
            return Dbcontext.Add(entity).Entity;
        }

        public void SaveChanges()
        {
            Dbcontext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Dbcontext.Update(entity);
        }
    }
}