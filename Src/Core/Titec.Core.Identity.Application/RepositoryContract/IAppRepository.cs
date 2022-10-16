using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IAppRepository : IGenericRepository<AppEntity, int>
    {
        Task<AppEntity> AddAsync(AppEntity entity);
        Task<IList<AppBaseViewModel>> GetAllAsync();
        Task<AppEntity> GetEntityById(int appId);
        Task RemoveByIdAsync(int id);
        Task<AppEntity> EditAsync(AppEntity entity);
        Task<AppBaseViewModel> GetByIdAsync(int AppId);
        //===============

        Task<List<RoleWithDetailBaseViewModel>> GetRollesAndPermissionsOfApp(string AppName);
        Task<bool> IsAppExist(string appName);
    }
}
