using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

public abstract class AbstractCommandEntityRepository<TEntity, TId> : Persistence.Repositories.ICommandEntityRepository<TEntity, TId>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
{
    protected readonly DbContext dbContext;

    public AbstractCommandEntityRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected virtual IQueryable<TEntity> GetQueryableDbSet() => dbContext.Set<TEntity>();

    public TEntity Get(TId id)
    {
        return GetQueryableDbSet().First(GetKeySelector(id));
    }

    public virtual TEntity Add(TEntity entity, string author, bool commit = true)
    {
        dbContext.Set<TEntity>().Add(entity);

        if (commit)
        {
            if (dbContext is ITraceableDbContext)
            {
                (dbContext as ITraceableDbContext).SaveChanges(author, commit);
            }
            else
            {
                dbContext.SaveChanges(commit);
            }
        }

        return entity;
    }

    public virtual TEntity Update(TEntity entity, string author, bool commit = true)
    {
        var entry = dbContext.Entry(entity);

        entry.State = EntityState.Modified;

        if (commit)
        {
            if (dbContext is ITraceableDbContext)
            {
                (dbContext as ITraceableDbContext).SaveChanges(author, commit);
            }
            else
            {
                dbContext.SaveChanges(commit);
            }
        }

        return entity;
    }

    public virtual void Delete(TEntity entity, string author, bool commit = true)
    {
        dbContext.Set<TEntity>().Remove(entity);

        if (commit)
        {
            if (dbContext is ITraceableDbContext)
            {
                (dbContext as ITraceableDbContext).SaveChanges(author, commit);
            }
            else
            {
                dbContext.SaveChanges(commit);
            }
        }
    }

    protected virtual Func<TEntity, bool> GetKeySelector(TId key)
    {
        return e => e.Id.Equals(key);
    }
}