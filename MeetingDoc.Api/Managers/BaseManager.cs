using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MeetingDoc.Api.Data;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Managers
{
    public abstract class BaseManager<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel, new()
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IBaseValidator<TViewModel> Validator { get; private set; }
        protected abstract IRepository<TEntity> Repository { get; }

        #region Constructor
        public BaseManager(IUnitOfWork unitOfWork, IBaseValidator<TViewModel> validator)
        {
            UnitOfWork = unitOfWork;
            Validator = validator;
        }
        #endregion

        #region Public Method(s)
        public virtual async Task<TViewModel> GetAsync(int id)
        {
            TEntity entity = await Repository.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            return ToViewModel(entity);
        }

        public async Task<PagedList<TViewModel>> GetAsync(BaseCriteria<TViewModel> criteria)
        {
            IQueryable<TEntity> query = GetByCriteria(criteria);
            return await ToPagedListAsync(query, criteria.PageSize, criteria.PageNumber);
        }

        public virtual async Task AddAsync(TViewModel viewModel, int operatedBy)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            var validateResult = Validator.ValidateBeforeAdd(viewModel);
            if (!validateResult.IsValid)
            {
                // TODO: handle invalid case
            }

            TEntity entity = ToEntity(viewModel);
            entity.CreatedBy = operatedBy;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = operatedBy;
            entity.UpdatedDate = DateTime.Now;

            await Repository.InsertAsync(entity);
            await UnitOfWork.SaveChangeAsync();
        }

        public virtual async Task UpdateAsync(TViewModel viewModel, int operatedBy)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            var validateResult = Validator.ValidateBeforeUpdate(viewModel);
            if (!validateResult.IsValid)
            {
                // TODO: handle invalid case
            }

            TEntity entity = ToEntity(viewModel);
            entity.UpdatedBy = operatedBy;
            entity.UpdatedDate = DateTime.Now;


            await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveChangeAsync();
        }

        public virtual async Task DeleteAsync(int id, int operatedBy)
        {
            TEntity entity = await Repository.GetAsync(id);
            if (entity == null)
            {
                return;
            }

            entity.IsRemoved = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = operatedBy;

            await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Protected Method(s) 
        protected virtual async Task<PagedList<TViewModel>> ToPagedListAsync(
            IQueryable<TEntity> query, int pageSize, int pageNumber)
        {
            var count = await query.CountAsync();

            var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            var paging = new PagedList<TViewModel>(
                items.Select(entity => ToViewModel(entity)).ToList(),
                count, pageNumber, pageSize);

            return paging;
        }
        #endregion

        #region Abstract Method(s)
        protected abstract IQueryable<TEntity> GetByCriteria(BaseCriteria<TViewModel> criteria);
        protected abstract TEntity ToEntity(TViewModel viewModel);
        protected abstract TViewModel ToViewModel(TEntity entity);
        #endregion
    }
}