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
        public IUserRepository UserRepository { get { return UnitOfWork.UserRepository; } }

        public UserManager(IUnitOfWork unitOfWork, IUserValidator validator)
            : base(unitOfWork, validator)
        {
        }

        protected override IQueryable<User> GetByCriteria(BaseCriteria<UserViewModel> criteria)
        {
            return Repository.GetQuery();
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

        public override async Task UpdateAsync(UserViewModel viewModel, int operatedBy)
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

            User entity = ToEntity(viewModel);
            entity.CreatedBy = operatedBy;
            entity.CreatedDate = DateTime.Now;

            await UserRepository.UpdateAsync(entity);
            await UnitOfWork.SaveChangeAsync();
        }
    }
}