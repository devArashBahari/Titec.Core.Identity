using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.RoleAggregate
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionService _PermissionService;
        private readonly IUserRoleService _UserRoleService;
        private readonly IRolePermissionService _rolePermissionService;
        public RoleService(IRoleRepository RoleRepository, IPermissionService PermissionService, IUnitOfWork unitOfWork, IUserRoleService userRoleService, IRolePermissionService rolePermissionService)
        {
            roleRepository = RoleRepository;
            _PermissionService = PermissionService;
            _UserRoleService = userRoleService;
            _unitOfWork = unitOfWork;

            _rolePermissionService = rolePermissionService;
        }
        public async Task<IEnumerable<RoleDetailWithPermissions>> GetAllWithDetails(string appName)
        {
            var result = await roleRepository.GetWithDetailAsync(x => x.App.Name.ToLower() == appName.ToLower());
            return result;
        }
        public async Task AddRoleAndPermissions(PermissionRoleAddCommandViewModel model)
        {

            if (!await roleRepository.RoleExist(model.roleId))
            {
                throw new EntityNotFoundException("این نقش وجود ندارد");

            }
            if (!await _PermissionService.PermissionExist(model.permissionId))
            {

                throw new EntityNotFoundException("این دسترسی ها موجود نیست");

            }
            if (model.permissionId.Count == 0)
            {
                throw new EntityNotNullException("دسترسی نمیتواند خالی باشد");
            }
            if (await _rolePermissionService.RoleHasePermissions(model.roleId, model.permissionId))
            {
                throw new DuplicateException("دسترسی های اضافه شده تکراری می با شد");

            }
            await _PermissionService.AddPermissionsToRole(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRolsToApp(AddRolsToAppCommandViewModel model)
        {
            await roleRepository.AddRolsToApp(model);
            //await _unitOfWork.SaveAsync();
        }

        public async Task RemoveByIdAsync(int roleId)
        {
            await roleRepository.RemoveByIdAsync(roleId);
            await _unitOfWork.SaveAsync();
            return;
        }
        public async Task EditRolesUser(int UserId, UpdateRolesOfUserAddCommandModel roleToUser)
        {
            if (!await RolsExist(roleToUser.roleIds))
            {
                throw new EntityNotFoundException("چنین نقشی موجود نمی باشد");
            }

            await _UserRoleService.EditRolesUser(UserId, roleToUser);
            await _unitOfWork.SaveAsync();
        }
        public async Task<RolePermissionBaseViewModel> GetRolAndPermissionById(int roleId)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            var permissions = await _PermissionService.PermissionsRole(role.Id);
            return new RolePermissionBaseViewModel()
            {
                Alias = role.Alias,
                Description = role.Description,
                Name = role.Name,
                PermissionTitle = permissions,
                Id = roleId,
            };
        }
        public async Task<IList<RoleBaseViewModel>> GetAllAsync()
        {
            return await roleRepository.GetAllAsync();
        }

        public async Task<bool> RoleExist(int RoleId)
        {
            return await roleRepository.RoleExist(RoleId);
        }

        public async Task<bool> RolsExist(List<int> RoleIds)
        {
            return await roleRepository.RolsExist(RoleIds);
        }

        public async Task UpdateRolePermissions(UpdatePermissionsOfRoleAddCommandModel roleAddCommand)
        {

            await _rolePermissionService.UpdatePermissionsRole
                   ((PermissionRoleAddCommandViewModel)roleAddCommand);
            await _unitOfWork.SaveAsync();
        }

        public async Task<RoleBaseViewModel> GetByIdAsync(int roleId)
        {
            var res = await roleRepository.GetByIdAsync(roleId);

            return (RoleBaseViewModel)res;
        }

        public async Task<RoleBaseViewModel> AddAsync(RoleCommandModel command)
        {
            if (await roleRepository.RoleExistByNAme(command.Name))
            {
                throw new DuplicateException("نقش تکراری با این نام موجود می باشد");
            }
            if (await roleRepository.RoleExistByAllias(command.Alias))
            {
                throw new DuplicateException("نقش تکراری با این alias موجود می باشد");
            }

            var roleEntiry = (RoleEntity)command;
            await roleRepository.AddAsync(roleEntiry);
            await _unitOfWork.SaveAsync();
            return (RoleBaseViewModel)roleEntiry;
        }

        public async Task<RoleBaseViewModel> EditAsync(RoleCommandModel command, int RoleId)
        {
            var role = await roleRepository.GetByIdAsync(RoleId);
            var compairRoleIdByName = await roleRepository.FindRoleIdByName(command.Name);
            var compairRoleIdByAllias = await roleRepository.FindRoleIdByAllias(command.Alias);

            if (role == null)
            {
                throw new EntityNotFoundException("اطلاعات یافت نشد");
            }
            if (role.Name == command.Name && role.Id == RoleId && role.Alias == command.Alias)
            {
                role.Name = command.Name;
                role.Alias = command.Alias;
                role.Description = command.Description;
                var result = await roleRepository.EditAsync(role);
                await _unitOfWork.SaveAsync();
                return (RoleBaseViewModel)result;
            }
            if (await roleRepository.RoleExistByNAme(command.Name) && compairRoleIdByName != role.Id)
            {
                throw new DuplicateException("نقش تکراری با این نام موجود می باشد");
            }
            if (await roleRepository.RoleExistByAllias(command.Alias) && compairRoleIdByAllias != role.Id)
            {
                throw new DuplicateException("نقش تکراری با این alias موجود می باشد");
            }
            role.Name = command.Name;
            role.Alias = command.Alias;
            role.Description = command.Description;
            var res = await roleRepository.EditAsync(role);
            await _unitOfWork.SaveAsync();
            return (RoleBaseViewModel)res;
        }

    }
}
