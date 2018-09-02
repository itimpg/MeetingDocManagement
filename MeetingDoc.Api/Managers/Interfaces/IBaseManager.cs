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
        Task<PagedList<TViewModel>> GetAsync(BaseCriteria<TViewModel> criteria);
        Task AddAsync(TViewModel viewModel, int operatedBy);
        Task UpdateAsync(TViewModel viewModel, int operatedBy);
        Task DeleteAsync(object id, int operatedBy);
    }
}