using System.Collections.Generic;

namespace Library.Common.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetOne(int id);

        ICollection<TEntity> GetAll();

        TEntity Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void SaveChanges();
    }
}