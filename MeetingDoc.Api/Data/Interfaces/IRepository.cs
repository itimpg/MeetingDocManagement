using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeetingDoc.Api.Models;

namespace MeetingDoc.Api.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<TEntity> GetAsync(object id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition);
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> condition);
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task UpdateAsync(TEntity entityToUpdate);
    }
}