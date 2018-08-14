using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IBaseManager<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel
    {
        Task<TViewModel> GetAsync(object id);
        PagedResult<TViewModel> Get(BaseCriteria<TViewModel> criteria);
        Task AddAsync(TViewModel viewModel);
        Task UpdateAsync(TViewModel viewModel);
        Task DeleteAsync(object id);
    }
}