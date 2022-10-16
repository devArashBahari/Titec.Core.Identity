using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IRolePermissionRepository
    {
        Task<UpdatePermissionsOfRoleBaseViewModel> UpdatePermissionsRole(PermissionRoleAddCommandViewModel model);
        Task<List<PermissionBaseViewModel>> PermissionsRole(int roleId);
        Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model);
        Task<Boolean> RoleHasePermissions(int RoleId, List<int> PermissionIds);
        Task<bool> RoleExistFromRolePermission(int RoleId);
    }
}
