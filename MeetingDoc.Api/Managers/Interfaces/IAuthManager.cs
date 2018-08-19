using System.Threading.Tasks;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IAuthManager
    {
        Task<UserViewModel> RegisterAsync(UserViewModel userRegisterViewModel, string password);
        Task<AuthViewModel> LoginAsync(string username, string password);
        Task<bool> IsUserExistsAsync(string username);
    }
}