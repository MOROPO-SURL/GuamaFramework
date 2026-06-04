using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.Framework.Guama.Core.Persistence.Repositories
{
    public interface IQueryEntityRepository<TEntity, TId> where TEntity : class, Entities.Contract.IEntity<TId>
    {
        TEntity Get(TId id);

        TEntity FindOneBy(Expression<Func<TEntity, bool>> criteria);

        TSpecializationEntity FindOneBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> criteria);

        IQueryable<TSpecializationEntity> FindBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;

        IQueryable<TEntity> GetAll();

        IQueryable<TSpecializationEntity> GetAll<TSpecializationEntity>() where TSpecializationEntity : class, TEntity;

        List<TEntity> GetPage(int page, int limit);

        int Count();

        int Count<TSpecializationEntity>() where TSpecializationEntity : class, TEntity;

        int Count(Expression<Func<TEntity, bool>> criteria);

        int Count<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity;
    }
}