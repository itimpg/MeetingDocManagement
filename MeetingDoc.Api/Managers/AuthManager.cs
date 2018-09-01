using System;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Helpers;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.Models;
using MeetingDoc.Api.ViewModels;

namespace MeetingDoc.Api.Managers
{
    public class AuthManager : IAuthManager
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _repository;
        private IEmailManager _emailManager;

        public AuthManager(IUnitOfWork unitOfWork, IEmailManager emailManager)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<User>();
            _emailManager = emailManager;
        }

        public async Task ChangePassword(int userId, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = await _repository.GetAsync(userId);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UpdatedBy = user.Id;
            user.UpdatedDate = DateTime.Now;

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task ResetPassword(string email)
        {
            var newPassword = PasswordGenerator.GeneratePassword(true, true, true, true, true, 8);

            var user = await _repository.GetAsync(x => x.Email == email);
            await ChangePassword(user.Id, newPassword);

            _emailManager.SendEmail(new EmailViewModel
            {
                EmailTo = email,
                Subject = "Your password was changed.",
                Body = $"Your new password is {newPassword}"
            });
        }

        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await _repository.IsExistsAsync(x => x.Email == username);
        }

        public async Task<UserViewModel> LoginAsync(string username, string password)
        {
            var user = await _repository.GetAsync(x => x.Email == username && x.IsActive && !x.IsRemoved);
            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Position = user.Position,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Level = (int)user.Level,
                LevelText = user.Level.ToString(),
                IsActive = user.IsActive
            };
        }

        public async Task<UserViewModel> RegisterAsync(UserViewModel viewModel, string password, int userId)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Position = viewModel.Position,
                Email = viewModel.Email,
                PhoneNo = viewModel.PhoneNo,
                Level = (UserLevel)viewModel.Level,
                IsActive = viewModel.IsActive,

                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                UpdatedBy = userId,
                UpdatedDate = DateTime.Now,
            };

            await _repository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();

            return viewModel;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}