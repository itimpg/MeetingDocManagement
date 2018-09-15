using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Data.Repositories.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.Validators.Interfaces;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class UserManager : BaseManager<User, UserViewModel>, IUserManager
    {
        protected override IRepository<User> Repository => UnitOfWork.UserRepository;

        public UserManager(IUnitOfWork unitOfWork, IUserValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<User> GetByCriteria(BaseCriteria<UserViewModel> criteria)
        {
            return Repository.GetQuery().Where(x => !x.IsRemoved);
        }

        protected override User ToEntity(UserViewModel viewModel)
        {
            return new User
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Position = viewModel.Position,
                Email = viewModel.Email,
                PhoneNo = viewModel.PhoneNo,
                Level = (UserLevel)viewModel.Level,
                IsActive = viewModel.IsActive
            };
        }

        protected override UserViewModel ToViewModel(User entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Position = entity.Position,
                Email = entity.Email,
                PhoneNo = entity.PhoneNo,
                Level = (int)entity.Level,
                LevelText = entity.Level.ToString(),
                IsActive = entity.IsActive
            };
        }
    }
}