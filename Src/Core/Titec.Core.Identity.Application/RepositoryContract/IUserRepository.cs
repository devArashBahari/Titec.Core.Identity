using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IUserRepository
    {
        Task<List<UserForRoleBaseViewModel>> GetAllUsers();
        Task<UserEntity> RegisterUser(UserEntity user);
        Task<bool> IsUserExistsByEmail(string email);
        Task<bool> IsUserExistsByUsername(string username);
        Task<bool> IsUserExistsById(int userId);
        Task<TokenResponseViewModel> LoginUser(LoginAddCommandModel login);
        Task<UserForRoleBaseViewModel> GetUserByEmail(string email);
        Task<UserForRoleBaseViewModel> UpdateUser(EditUserRoleAddCommandModelViewModel roleToUser);
        Task<bool> IsUserExistsByMobileNo(string mobileNo);
        Task FilterAll(string term);
        Task Sort(string filed, string sortDirection);
        Task<List<GetUsersBaseViewModel>> GetStudents(int page, int pageSize);
        Task GenerateOTP(GenerateOtpAddCommandModel model);
        Task<TokenResponseViewModel> LoginWithOtp(LoginWithOtpAddCommandModel model);
        Task<UserEntity> GetUserWithUserNameAndEmail(LoginAddCommandModel model);
        Task<UserEntity> GetUserById(int userId);
        Task<UserEntity> EditUser(UserEntity user);
        Task<int> FindUserIdByUserName(string UserName);
        Task<int> FindUserIdByEmail(string Email);
        Task<int> FindUserIdByMobileNumber(string MobileNo);
        Task<IEnumerable<UsersWithRolsBaseViewModel>> GetUsersWithRols();
        Task<List<object>> GetRowLevelAccessList();
    }
}
