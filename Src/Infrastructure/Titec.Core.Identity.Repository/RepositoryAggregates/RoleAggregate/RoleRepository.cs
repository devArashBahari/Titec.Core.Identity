using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.RoleAggregate
{
    public class RoleRepository : GenericRepository<RoleEntity, int>, IRoleRepository
    {
        public RoleRepository(BaseDbContext dbContext, ITitecIdentity titecIdentity) : base(dbContext, titecIdentity)
        {
        }

        public async Task<IEnumerable<RoleDetailWithPermissions>> GetWithDetailAsync(Expression<Func<RoleEntity, bool>> predicate = null)
        {
            var roles = await base._dbSet
                                           .Include(c => c.App)
                                           .Include(c => c.RolePermissions)
                                                .ThenInclude(c => c.Permission)
                                           .Where(predicate)
                                           .ToListAsync();



            var result = roles
            .Select(x => new RoleDetailWithPermissions()
            {
                Id = x.Id,
                Title = x.Name,
                Alias = x.Alias,
                IsAdmin = x.IsAdmin,
                IsSuperAdmin = x.IsSuperAdmin,
                AppId = x.AppId,
                Permissions =
                    x.RolePermissions.Select(p => p.Permission)
                    .GroupBy(g => g.Id)
                    .Select(k => k.FirstOrDefault())
                    .OrderBy(q => q.Id)
                    .Select(y =>
                    new PermissionSummaryViewModel()
                    {
                        Id = y.Id,
                        Alias = y.Alias,
                        Title = y.Title,
                        Path = y.Path,
                        MethodId = (byte)y.MethodId
                    })
            })
            .ToList();

            return result;
        }

        public async Task<RoleEntity> AddAsync(RoleEntity entity)
        {
            await base.AddAsync(entity);
            return entity;
        }

        //public async Task AddRolesToUser(RoleUserAddCommandModel roleToUser)
        //{
        //    foreach (var roleId in roleToUser.roleIds)
        //    {
        //       await base.AddAsync(new UserRoleEntity()
        //        {
        //            RoleId = roleId,
        //            UserId = roleToUser.userId
        //        });
        //    }
        //}

        public async Task RemoveByIdAsync(int roleId)
        {
            var role = await base._dbSet.FirstOrDefaultAsync(c => c.Id == roleId && c.IsDeleted == false);
            if (role == null)
            {
                throw new EntityNotFoundException("اطلاعات یافت نشد");
            }

            role.IsDeleted = true;
            await base.EditAsync(role);
        }

        public async Task<RoleEntity> EditAsync(RoleEntity model)
        {
            await base.EditAsync(model);

            return model;

        }






        public async Task<RoleEntity> GetByIdAsync(int roleId)
        {
            var role = await base._dbSet.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == roleId);

            return role;
        }

        public async Task<List<RoleBaseViewModel>> GetAllAsync()
        {
            var roles = await base._dbSet.Where(p => p.IsDeleted == false).Select(c => (RoleBaseViewModel)c).ToListAsync();

            return roles;
        }

        public async Task<RoleBaseViewModel> UpdateRole(UpdateRolePermissionAddCommandModel roleAddCommand)
        {
            var Role = await base._dbSet.FirstOrDefaultAsync(x => x.Id == roleAddCommand.roleId);
            if (Role == null)
            {
                //return new RoleBaseViewModel()
                //{
                //    Massege = "چنین نقشی وجود ندارد"
                //};
            }
            Role.Name = roleAddCommand.Name;
            Role.Description = roleAddCommand.Description;
            Role.Alias = roleAddCommand.Alias;
            Role.LastEditDate = DateTime.Now;
            base._dbSet.Update(Role);

            return (RoleBaseViewModel)Role;
        }
        public async Task<Boolean> RoleExist(int RoleId)
        {
            return await base._dbSet.AnyAsync(x => x.Id == RoleId);
        }
        public async Task<Boolean> RolsExist(List<int> RoleIds)
        {

            foreach (var item in RoleIds)
            {
                var Exist = await base._dbSet.AnyAsync(c => c.Id == item && c.IsDeleted == false);
                if (!Exist)
                {
                    return false;
                }
            }
            return true;

        }

        public async Task<bool> RoleExistByNAme(string Name)
        {
            return await base._dbSet.AnyAsync(c => c.Name == Name && c.IsDeleted == false);
        }

        public async Task<bool> RoleExistByAllias(string Allias)
        {
            return await base._dbSet.AnyAsync(c => c.Alias == Allias && c.IsDeleted == false);
        }

        public async Task AddRolsToApp(AddRolsToAppCommandViewModel model)
        {
            var role = new RoleEntity();
            foreach (var item in model.RolsIDs)
            {


                role.AppId = model.AppId;
                role.Id = item;

            }
            base._dbSet.Update(role);
        }

        public async Task<int> FindRoleIdByName(string Name)
        {
            return await base._dbSet.Where(c => c.IsDeleted == false && c.Name == Name).Select(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<int> FindRoleIdByAllias(string allias)
        {
            return await base._dbSet.Where(c => c.IsDeleted == false && c.Alias == allias).Select(c => c.Id).FirstOrDefaultAsync();
        }
    }
}
