using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MOROPO.Framework.Guama.Core.Persistence.Repositories
{
    public interface IEntityRepository<TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
        TEntity Get(TId id);

        TEntity FindOneBy(Expression<Func<TEntity, bool>> criteria);

        TSpecializationEntity FindOneBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> criteria);

        IQueryable<TSpecializationEntity> FindBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;

        IQueryable<TEntity> GetAll();

        IQueryable<TSpecializationEntity> GetAll<TSpecializationEntity>() where TSpecializationEntity : class, TEntity;

        List<TEntity> GetPage(int page, int limit);

        TEntity Add(TEntity entity, string author, bool commit = true);

        TEntity Update(TEntity entity, string author, bool commit = true);

        void Delete(TEntity entity, string author, bool commit = true);

        int Count();

        int Count<TSpecializationEntity>() where TSpecializationEntity : class, TEntity;

        int Count(Expression<Func<TEntity, bool>> criteria);

        int Count<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;
    }
}
