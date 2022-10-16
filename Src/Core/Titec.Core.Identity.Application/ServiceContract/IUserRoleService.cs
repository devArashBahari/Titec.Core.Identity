using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IUserRoleService
    {
        Task<RoleUserBaseViewModel> AddRolesToUser(RoleUserAddCommandModel roleToUser);
        Task EditRolesUser(int UserId, UpdateRolesOfUserAddCommandModel roleToUser);
        Task<Boolean> UserHaseRols(int UserId, List<int> RoleIds);
        
    }
}
