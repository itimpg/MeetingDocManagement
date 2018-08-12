using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingDoc.Api.Data;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public abstract class BaseManager<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class, new()
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<TEntity> _repository;

        #region Constructor
        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<TEntity>();
        }
        #endregion

        #region Public Method(s)
        public virtual async Task<TViewModel> GetAsync(object id)
        {
            TEntity entity = await _repository.GetAsync(id);
            return ToViewModel(entity);
        }

        public PagedResult<TViewModel> Get(BaseSearchCriteria<TViewModel> criteria)
        {
            IQueryable<TEntity> query = GetByCriteria(criteria);
            return ToPaging(query, criteria.PageSize, criteria.PageIndex);
        }

        public virtual async Task SaveAsync(TViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            TEntity entity = ToEntity(viewModel);
            entity.CreatedBy = ""; // TODO: get current user here;
            entity.CreatedDate = DateTime.Now;

            if (entity.Id == 0)
            {
                entity.UpdatedBy = ""; // TODO: get current user here;
                entity.UpdatedDate = DateTime.Now;
                await _repository.InsertAsync(entity);
            }
            else
            {
                _repository.Update(entity);
            }

            await _unitOfWork.SaveChangeAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            TEntity entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return;
            }

            entity.IsRemoved = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = ""; // TODO: get current user here;

            _repository.Update(entity);
            await _unitOfWork.SaveChangeAsync();
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
            paging.TotalPage = (paging.TotalRecord - 1) / (pageSize + 1);

            return paging;
        }
        #endregion


        #region Abstract Method(s)
        protected abstract IQueryable<TEntity> GetByCriteria(BaseSearchCriteria<TViewModel> criteria);
        protected abstract TEntity ToEntity(TViewModel viewModel);
        protected abstract TViewModel ToViewModel(TEntity entity);
        #endregion
    }
}