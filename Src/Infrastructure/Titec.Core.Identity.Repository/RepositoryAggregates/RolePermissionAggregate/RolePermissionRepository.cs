using Microsoft.EntityFrameworkCore;
using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.RolePermissionAggregate
{
    public class RolePermissionRepository : GenericRepository<RolePermissionEntity, int>, IRolePermissionRepository
    {
        public RolePermissionRepository(BaseDbContext dbContext, ITitecIdentity titecIdentity) : base(dbContext, null)
        {
        }
        public async Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model)
        {
            foreach (var p in model.permissionId)
            {
                await base._dbSet.AddAsync(new RolePermissionEntity()
                {
                    PermissionId = p,
                    RoleId = model.roleId
                });
            }
        }
        public async Task<List<PermissionBaseViewModel>> PermissionsRole(int roleId)
        {
            var permission = await base._dbSet.Include(p => p.Permission)
                .Where(r => r.RoleId == roleId && r.IsDeleted==false).ToListAsync();
            List<PermissionBaseViewModel> permissions = new List<PermissionBaseViewModel>();
            
            foreach (var item in permission)
            {
                permissions.Add((PermissionBaseViewModel)item);
            }
            return permissions;
        }

        public async Task<bool> RoleExistFromRolePermission(int RoleId)
        {
          return  await base._dbSet.Include(p=>p.Role).AnyAsync(p=>p.Role.Id == RoleId);
        }

        public async Task<bool> RoleHasePermissions(int RoleId, List<int> PermissionIds)
        {
            foreach (var item in PermissionIds)
            {
                if (await _dbSet.AnyAsync(c => c.RoleId == RoleId && c.PermissionId == item))
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<UpdatePermissionsOfRoleBaseViewModel> UpdatePermissionsRole(PermissionRoleAddCommandViewModel model)
        {
            if (model.roleId==null)
            {
                return new UpdatePermissionsOfRoleBaseViewModel()
                {
                    massege = "نقش وجود ندارد"
                };
            }

            base._dbSet.Where(p => p.RoleId == model.roleId)
                 .ToList().ForEach(p => base._dbSet.Remove(p));

            await AddPermissionsToRole(model);
            return new UpdatePermissionsOfRoleBaseViewModel();
        }
    }
}
