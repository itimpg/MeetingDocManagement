using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected DataContext DbContext { get; private set; }
        protected DbSet<TEntity> Dbset { get; private set; }

        public Repository(DataContext dbContext)
        {
            DbContext = dbContext;
            Dbset = dbContext.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetQuery(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = Dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                orderBy(query);
            }

            return query;
        }

        public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Dbset.AnyAsync(condition);
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await Dbset.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Dbset.FirstOrDefaultAsync(condition);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await Dbset.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(object id)
        {
            var entity = await GetAsync(id);
            Dbset.Remove(entity);
        }

        public virtual Task UpdateAsync(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}