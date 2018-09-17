using System.Threading.Tasks;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IBaseManager<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel, new()
    {
        Task<TViewModel> GetAsync(int id);
        Task<PagedList<TViewModel>> GetAsync(BaseCriteria<TViewModel> criteria);
        Task AddAsync(TViewModel viewModel, int operatedBy);
        Task UpdateAsync(TViewModel viewModel, int operatedBy);
        Task DeleteAsync(int id, int operatedBy);
    }
}