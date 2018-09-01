using System.Threading.Tasks;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers.Interfaces
{
    public interface IAuthManager
    {
        Task<UserViewModel> RegisterAsync(UserViewModel userRegisterViewModel, string password, int userId);
        Task<UserViewModel> LoginAsync(string username, string password);
        Task<bool> IsUserExistsAsync(string username);
        Task ChangePassword(int userId, string password);
         Task ResetPassword(string email);
    }
}