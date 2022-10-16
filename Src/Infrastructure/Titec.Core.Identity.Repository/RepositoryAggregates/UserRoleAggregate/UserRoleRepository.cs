using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.UserRoleAggregate
{
    public class UserRoleRepository : GenericRepository<UserRoleEntity, Guid>, IUserRoleRepository
    {
        private readonly BaseDbContext _dbContext;
        public UserRoleRepository(BaseDbContext dbContext, ITitecIdentity? titecIdentity) : base(dbContext, titecIdentity)
        {
        }
        public async Task AddRolesToUser(RoleUserAddCommandModel roleToUser)
        {


            foreach (var roleId in roleToUser.roleIds)
            {
                await base.AddAsync(new UserRoleEntity()
                {
                    RoleId = roleId,
                    UserId = roleToUser.userId
                });
            }
        }

        public async Task EditRolesUser(int UserId, UpdateRolesOfUserAddCommandModel roleToUser)
        {
            //Delete All Roles User
            base._dbSet.Where(r => r.UserId == UserId).ToList().ForEach(r => base._dbSet.Remove(r));

            //Add New Roles
            var editUserRole = new RoleUserAddCommandModel()
            {
                userId = UserId,
                roleIds = roleToUser.roleIds,
            };
            await AddRolesToUser(editUserRole);
        }



        public async Task<bool> UserHaseRols(int UserId, List<int> RoleIds)
        {
            foreach (var item in RoleIds)
            {
                if (await _dbSet.AnyAsync(c => c.UserId==UserId && c.RoleId==item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
