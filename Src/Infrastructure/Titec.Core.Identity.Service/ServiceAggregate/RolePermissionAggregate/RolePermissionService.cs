using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.RolePermissionAggregate
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRolePermissionRepository _RolePermissionrepository;
        //private readonly IRoleService _IRoleService;


        public RolePermissionService(IRolePermissionRepository RolePermissionRepository, IUnitOfWork unitOfWork/*,IRoleService roleService*/)
        {
            _RolePermissionrepository = RolePermissionRepository;
            //_IRoleService = roleService;
            _unitOfWork = unitOfWork;
        }
        public async Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model)
        {
            await _RolePermissionrepository.AddPermissionsToRole(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<PermissionBaseViewModel>> PermissionsRole(int roleId)
        {
            return await _RolePermissionrepository.PermissionsRole(roleId);
        }

        public async Task<bool> RoleExistFromRolePermission(int RoleId)
        {
           return await _RolePermissionrepository.RoleExistFromRolePermission(RoleId);
        }

        public async Task<bool> RoleHasePermissions(int RoleId, List<int> PermissionIds)
        {
            return await _RolePermissionrepository.RoleHasePermissions(RoleId, PermissionIds);
        }
        public async Task UpdatePermissionsRole(PermissionRoleAddCommandViewModel model)
        {
            if (! await RoleExistFromRolePermission(model.roleId))
            {
                throw new EntityNotFoundException("چنین نقشی وجود ندارد");
            }   
            await _RolePermissionrepository.UpdatePermissionsRole(model);
        }
    }
}
