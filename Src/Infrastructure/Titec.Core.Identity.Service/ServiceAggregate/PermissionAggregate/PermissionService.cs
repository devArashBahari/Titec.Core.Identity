
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.PermissionAggregate
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _Permissionrepository;
        private readonly IRolePermissionRepository _RolePermissionrepository;

        private readonly IUnitOfWork _unitOfWork;
        public PermissionService(IPermissionRepository PermissionRepository, IUnitOfWork unitOfWork, IRolePermissionRepository rolePermissionRepository)
        {
            _Permissionrepository = PermissionRepository;
            _unitOfWork = unitOfWork;
            _RolePermissionrepository = rolePermissionRepository;
        }

        public async Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model)
        {
            await _Permissionrepository.AddPermissionsToApp(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model)
        {
            await _RolePermissionrepository.AddPermissionsToRole(model);
        }

        //public bool CheckPermission(CheckPermissionAddCommandModel model)
        //{
        //  return  _Permissionrepository.CheckPermission(model);
        //}

        public async Task<List<PermissionBaseViewModel>> GetPermissions()
        {
            return await _Permissionrepository.GetPermissions();
        }

        public async Task<bool> PermissionExist(List<int> PermissionId)
        {
            return await _Permissionrepository.PermissionExist(PermissionId);
        }

        public async Task<List<PermissionBaseViewModel>> PermissionsRole(int roleId)
        {
            return await _RolePermissionrepository.PermissionsRole(roleId);
        }
    }
}
