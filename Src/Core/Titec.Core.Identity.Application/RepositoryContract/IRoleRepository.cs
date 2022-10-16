using System.Linq.Expressions;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IRoleRepository : IGenericRepository<RoleEntity, int>
    {
        Task<IEnumerable<RoleDetailWithPermissions>> GetWithDetailAsync(Expression<Func<RoleEntity, bool>> predicate = null);
        Task<RoleEntity> AddAsync(RoleEntity entity);
        Task<List<RoleBaseViewModel>> GetAllAsync();
        Task RemoveByIdAsync(int roleId);

        Task<int> FindRoleIdByName(string Name);
        Task<int> FindRoleIdByAllias(string allias);
        Task<RoleEntity> EditAsync(RoleEntity model);
        Task<RoleEntity> GetByIdAsync(int roleId);





        //Task AddRolesToUser(RoleUserAddCommandModel roleToUser);
        Task<RoleBaseViewModel> UpdateRole(UpdateRolePermissionAddCommandModel roleAddCommand);
        //Task EditRolesUser(EditUserRoleAddCommandModelViewModel roleToUser);
        Task<Boolean> RoleExist(int RoleId);
        Task<Boolean> RolsExist(List<int> RoleIds);
        Task<Boolean> RoleExistByNAme(string Name);
        Task<Boolean> RoleExistByAllias(string Allias);
        Task AddRolsToApp(AddRolsToAppCommandViewModel model);


    }
}
