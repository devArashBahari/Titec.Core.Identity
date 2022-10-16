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

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IUserService
    {
        Task<List<UserForRoleBaseViewModel>> GetAllUsers();
        Task<RegisterBaseViewModel> RegisterUser(RegisterAddCommandModel register);
        Task<bool> IsUserExistsByEmail(string email);
        Task<bool> IsUserExistsByUsername(string username);
        Task<TokenResponseViewModel> LoginUser(LoginAddCommandModel login);
        Task<UserForRoleBaseViewModel> GetUserByEmail(string email);
        Task<bool> IsUserExistsById(int userId);
        Task<UserForRoleBaseViewModel> UpdateUser(EditUserRoleAddCommandModelViewModel roleToUser);
        Task<List<GetUsersBaseViewModel>> FilterSortPagination(GetUserAddCommandModel model);
        Task GenerateOTP(GenerateOtpAddCommandModel model);
        Task<TokenResponseViewModel> LoginWithOtp(LoginWithOtpAddCommandModel model);
        Task AddCustomersToUser(AddCustomersToUserCommandViewModel model);
        Task<EditUserBaseViewModel> EditUser(EditUserCommandModel model, int userId);
        Task AddRoleToUser(RoleUserAddCommandModel model);
        Task UpdateCustomersToUser(AddCustomersToUserCommandViewModel model);
        Task<IEnumerable<UsersWithRolsBaseViewModel>> GetUsersWithRols();
        Task<List<object>> GetRowLevelAccessList();
    }
}

