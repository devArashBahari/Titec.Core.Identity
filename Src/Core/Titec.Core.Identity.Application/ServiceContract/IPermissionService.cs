
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IPermissionService
    {
        Task<List<PermissionBaseViewModel>> GetPermissions();
        Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model);
        Task<List<PermissionBaseViewModel>> PermissionsRole(int roleId);
        //bool CheckPermission(CheckPermissionAddCommandModel model);
        Task<Boolean> PermissionExist(List<int> PermissionId);
        Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model);
    }
}
