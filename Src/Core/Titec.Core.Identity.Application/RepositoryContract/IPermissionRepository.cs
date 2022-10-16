using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IPermissionRepository
    {
        Task<List<PermissionBaseViewModel>> GetPermissions();

        //bool CheckPermission(CheckPermissionAddCommandModel model);
        Task<Boolean> PermissionExist(List<int> PermissionId);
        Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model);
    }
}
