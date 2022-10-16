using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDetailWithPermissions>> GetAllWithDetails(string appName);

        Task<RoleBaseViewModel> AddAsync(RoleCommandModel command);
        Task<IList<RoleBaseViewModel>> GetAllAsync();
        Task<RoleBaseViewModel> EditAsync(RoleCommandModel command, int RoleId);
        Task RemoveByIdAsync(int roleId);
        Task<RoleBaseViewModel> GetByIdAsync(int roleId);
        //  ===================

        //Task AddRolesToUser(RoleUserAddCommandModel roleToUser);
        Task AddRoleAndPermissions(PermissionRoleAddCommandViewModel roleAddCommand);
        Task<RolePermissionBaseViewModel> GetRolAndPermissionById(int roleId);
        Task UpdateRolePermissions(UpdatePermissionsOfRoleAddCommandModel roleAddCommand);
        Task EditRolesUser(int UserId, UpdateRolesOfUserAddCommandModel roleToUser);
        Task<Boolean> RoleExist(int RoleId);
        Task<Boolean> RolsExist(List<int> RoleIds);
        Task AddRolsToApp(AddRolsToAppCommandViewModel model);


    }
}
