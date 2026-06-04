using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

public abstract class AbstractQueryEntityRepository<TEntity, TId> : Persistence.Repositories.IQueryEntityRepository<TEntity, TId>
    where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
{
    protected readonly IQueryable<TEntity> baseQueryableDbSet;

    public AbstractQueryEntityRepository(DbContext dbContext)
    {
        baseQueryableDbSet = dbContext.Set<TEntity>().AsNoTracking();
    }

    protected virtual IQueryable<TEntity> GetQueryableDbSet() => baseQueryableDbSet;

    protected virtual IQueryable<TSpecializationEntity> GetQueryableDbSet<TSpecializationEntity>()
    {
        return GetQueryableDbSet().Cast<TSpecializationEntity>();
    }

    public TEntity Get(TId id)
    {
        return GetQueryableDbSet().First(GetKeySelector(id));
    }

    public TEntity FindOneBy(Expression<Func<TEntity, bool>> criteria)
    {
        return GetQueryableDbSet().FirstOrDefault(criteria);
    }

    public TSpecializationEntity FindOneBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
    {
        return GetQueryableDbSet<TSpecializationEntity>().FirstOrDefault(criteria);
    }

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> criteria)
    {
        if (criteria == null) return GetQueryableDbSet();

        return GetQueryableDbSet().Where(criteria);
    }

    public IQueryable<TSpecializationEntity> FindBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
    {
        if (criteria == null) return GetQueryableDbSet<TSpecializationEntity>();

        return GetQueryableDbSet<TSpecializationEntity>().Where(criteria);
    }

    public IQueryable<TEntity> GetAll()
    {
        return GetQueryableDbSet();
    }

    public IQueryable<TSpecializationEntity> GetAll<TSpecializationEntity>() where TSpecializationEntity : class, TEntity
    {
        return GetQueryableDbSet<TSpecializationEntity>();
    }

    public List<TEntity> GetPage(int page, int limit)
    {
        return GetQueryableDbSet().Skip(page * limit).Take(limit).ToList();
    }

    public int Count()
    {
        return baseQueryableDbSet.Count();
    }

    public int Count<TSpecializationEntity>() where TSpecializationEntity : class, TEntity
    {
        return baseQueryableDbSet.Cast<TSpecializationEntity>().Count();
    }

    public int Count(Expression<Func<TEntity, bool>> criteria)
    {
        return baseQueryableDbSet.Count(criteria);
    }

    public int Count<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
    {
        return baseQueryableDbSet.Cast<TSpecializationEntity>().Count(criteria);
    }

    protected virtual Func<TEntity, bool> GetKeySelector(TId key)
    {
        return e => e.Id.Equals(key);
    }
}