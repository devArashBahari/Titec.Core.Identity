using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IAppService
    {
        Task<AppBaseViewModel> AddAsync(AppCommandModel model);
        Task<IList<AppBaseViewModel>> GetAllAsync();
        Task<AppBaseViewModel> EditAsync(AppCommandModel model, int AppId);
        Task RemoveByIdAsync(int id);
        Task<AppBaseViewModel> GetByIdAsync(int appId);
        //  ===================

        Task AddUserToApp(UserToAppAddCommandViewModel model);
        Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model);
        Task<bool> IsAppExist(string appName);
        Task AddRolsToApp(AddRolsToAppCommandViewModel model);
        Task<List<RoleWithDetailBaseViewModel>> GetRollesAndPermissionsOfApp(string AppName);

    }
}
