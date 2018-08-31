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

namespace MeetingDoc.Api.Managers
{
    public abstract class BaseManager<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IBaseValidator<TViewModel> Validator { get; private set; }
        protected virtual IRepository<TEntity> Repository { get { return UnitOfWork.GetRepository<TEntity>(); } }

        #region Constructor
        public BaseManager(IUnitOfWork unitOfWork, IBaseValidator<TViewModel> validator)
        {
            UnitOfWork = unitOfWork;
            Validator = validator;
        }
        #endregion

        #region Public Method(s)
        public virtual async Task<TViewModel> GetAsync(object id)
        {
            TEntity entity = await Repository.GetAsync(id);
            return ToViewModel(entity);
        }

        public PagedResult<TViewModel> Get(BaseCriteria<TViewModel> criteria)
        {
            IQueryable<TEntity> query = GetByCriteria(criteria);
            return ToPaging(query, criteria.PageSize, criteria.PageIndex);
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

            using (var scope = new TransactionScope())
            {
                TEntity entity = ToEntity(viewModel);
                entity.CreatedBy = operatedBy;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = operatedBy;
                entity.UpdatedDate = DateTime.Now;

                await Repository.InsertAsync(entity);
                await UnitOfWork.SaveChangeAsync();
                scope.Complete();
            }
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

            using (var scope = new TransactionScope())
            {
                TEntity entity = ToEntity(viewModel);
                entity.CreatedBy = operatedBy;
                entity.CreatedDate = DateTime.Now;

                Repository.Update(entity);
                await UnitOfWork.SaveChangeAsync();
                scope.Complete();
            }
        }

        public virtual async Task DeleteAsync(object id, int operatedBy)
        {
            using (var scope = new TransactionScope())
            {
                TEntity entity = await Repository.GetAsync(id);
                if (entity == null)
                {
                    return;
                }

                entity.IsRemoved = true;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = operatedBy;

                Repository.Update(entity);
                await UnitOfWork.SaveChangeAsync();
                scope.Complete();
            }
        }
        #endregion

        #region Protected Method(s)
        protected virtual PagedResult<TViewModel> ToPaging(IQueryable<TEntity> query, int pageSize, int pageIndex)
        {
            var paging = new PagedResult<TViewModel>()
            {
                CurrentPage = pageIndex,
                TotalRecord = query.Count(),
                Data = query.Skip(pageSize * pageIndex).Select(entity => ToViewModel(entity))
            };
            paging.TotalPage = pageSize == 0 ? 1 : (paging.TotalRecord - 1) / (pageSize + 1);
            if (paging.CurrentPage >= paging.TotalPage)
            {
                paging.CurrentPage = paging.TotalPage - 1;
            }

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