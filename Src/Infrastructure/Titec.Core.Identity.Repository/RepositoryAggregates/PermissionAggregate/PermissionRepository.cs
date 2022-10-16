using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.PermissionAggregate
{
    public class PermissionRepository : IPermissionRepository
    {

        private readonly BaseDbContext _dbContext;
        public PermissionRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task AddPermissionsToRole(PermissionRoleAddCommandViewModel model)
        //{
        //    foreach (var p in model.permissionId)
        //    {
        //        await _dbContext.RolePermissions.AddAsync(new RolePermissionEntity()
        //        {
        //            PermissionId = p,
        //            RoleId = model.roleId
        //        });
        //    }
        //}
        public async Task<Boolean> PermissionExist(List<int> PermissionId)
        {

            foreach (var item in PermissionId)
            {
                var Exist = await _dbContext.Permissions.AnyAsync(c => c.Id == item && c.isDeleted == false);
                if (!Exist)
                {
                    return false;
                }
            }
            return true;

        }

        public bool CheckPermission(CheckPermissionAddCommandModel model)
        {
            int userId = _dbContext.users.Single(u => u.UserName == model.userName).Id;


            List<int> UserRoles = _dbContext.userRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _dbContext.RolePermissions
                .Where(p => p.PermissionId == model.permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains((int)p));
        }

        public async Task<List<PermissionBaseViewModel>> GetPermissions()
        {
            var Permission = await _dbContext.Permissions.Where(p => p.isDeleted == false).Select(c => (PermissionBaseViewModel)c).ToListAsync();

            return Permission;
        }

        public async Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model)
        {
            foreach (var item in model.PermissionIDs)
            {
                await _dbContext.Permissions.AddAsync(new PermissionEntity()
                {
                    AppId = model.AppId,
                    Id = item
                });
            }
        }

        //public async Task<List<PermissionBaseViewModel>> PermissionsRole(Guid roleId)
        //{
        //    var permission = await  _dbContext.RolePermissions.Include(p => p.Permission)
        //        .Where(r => r.RoleId == roleId).ToListAsync();
        //    List<PermissionBaseViewModel> permissions = new List<PermissionBaseViewModel>();
        //    if (permission.Count==0) {
        //        permissions.Add(new PermissionBaseViewModel()
        //        {
        //            Massege="پرمیژن وجود ندارد"
        //        });
        //        return permissions;
        //    }
        //    foreach (var item in permission)
        //    {
        //        permissions.Add((PermissionBaseViewModel)item);
        //    }
        //    return permissions;

        //}

        //public async Task UpdatePermissionsRole(PermissionRoleAddCommandViewModel model)
        //{
        //    _dbContext.RolePermissions.Where(p => p.RoleId == model.roleId)
        //         .ToList().ForEach(p => _dbContext.RolePermissions.Remove(p));

        //   await AddPermissionsToRole(model);
        //}
    }
}
