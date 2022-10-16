using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.UserRoleAggregate
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRoleRepository _UserRolerepository;
        private readonly IUserService _UserService;


        public UserRoleService(IUserRoleRepository UserRoleRepository, IUnitOfWork unitOfWork, IUserService UserService)
        {
            _UserRolerepository = UserRoleRepository;
            _UserService = UserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<RoleUserBaseViewModel> AddRolesToUser(RoleUserAddCommandModel roleToUser)
        {
            if (! await _UserService.IsUserExistsById(roleToUser.userId))
            {
                return new RoleUserBaseViewModel()
                {
                    Massage = "کاربری یافت نشد"
                };
            }
            await _UserRolerepository.AddRolesToUser(roleToUser);
            await _unitOfWork.SaveAsync();
            return new RoleUserBaseViewModel()
            {
                Massage = null
            };
        }

        public async Task EditRolesUser(int UserId, UpdateRolesOfUserAddCommandModel roleToUser)
        {
      
            await _UserRolerepository.EditRolesUser(UserId,roleToUser);
            await _unitOfWork.SaveAsync();
            return ;
        }

 

        public async Task<bool> UserHaseRols(int UserId, List<int> RoleIds)
        {
            return await _UserRolerepository.UserHaseRols(UserId, RoleIds);
        }
    }
}
