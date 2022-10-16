using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.AppAggregate
{
    public class AppService : IAppService
    {
        private readonly IAppRepository appRepository;
        private readonly IPermissionService permissionService;
        private readonly IRoleService roleService;
        private readonly IAppUserService appUserService;
        private readonly IUnitOfWork _unitOfWork;
        public AppService(IAppRepository IAppRepository, IUnitOfWork unitOfWork, IAppUserService IAppUserService, IPermissionService IPermissionService, IRoleService IRoleService)
        {
            appRepository = IAppRepository;
            _unitOfWork = unitOfWork;
            appUserService = IAppUserService;
            permissionService = IPermissionService;
            roleService = IRoleService;
        }

        public async Task<AppBaseViewModel> AddAsync(AppCommandModel model)
        {
            if (await IsAppExist(model.Name))
            {
                throw new DuplicateException("نرم افزار تکراری با این نام موجود می باشد");
            }
            var appEntity = (AppEntity)model;
          var res=  await appRepository.AddAsync(appEntity);
            await _unitOfWork.SaveAsync();
            return (AppBaseViewModel)appEntity;
        }

        public async Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model)
        {
            await permissionService.AddPermissionsToApp(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRolsToApp(AddRolsToAppCommandViewModel model)
        {
            await roleService.AddRolsToApp(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddUserToApp(UserToAppAddCommandViewModel model)
        {
            await appUserService.AddUserToApp(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<AppBaseViewModel> EditAsync(AppCommandModel model, int AppId)
        {
            var entity=await appRepository.GetEntityById(AppId);
            if (entity == null)
            {
                throw new EntityNotFoundException("اطلاعات یافت نشد");
            }

            entity.Title = model.Title;
            entity.Name = model.Name;

            var data = await appRepository.EditAsync(entity);
            await _unitOfWork.SaveAsync();
            return (AppBaseViewModel)data;
        }

        public async Task<IList<AppBaseViewModel>> GetAllAsync()
        {
            return await appRepository.GetAllAsync();
        }

        public async Task<AppBaseViewModel> GetByIdAsync(int AppId)
        {
            return await appRepository.GetByIdAsync(AppId);
        }

        public async Task<List<RoleWithDetailBaseViewModel>> GetRollesAndPermissionsOfApp(string AppName)
        {
            return await appRepository.GetRollesAndPermissionsOfApp(AppName);
        }

        public async Task<bool> IsAppExist(string appName)
        {
            return await appRepository.IsAppExist(appName);
        }

        public async Task RemoveByIdAsync(int AppId)
        {
            await appRepository.RemoveByIdAsync(AppId);
            await _unitOfWork.SaveAsync();
            return;
        }
               
    }
}
