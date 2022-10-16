using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.AppUserAggregate
{
    public class AppUserService : IAppUserService
    {
       
        private readonly IAppUserRepository _IAppUserrepository;
        private readonly IUnitOfWork _unitOfWork;
        public AppUserService( IUnitOfWork unitOfWork, IAppUserRepository IAppUserrepository)
        {
            
            _unitOfWork = unitOfWork;
            _IAppUserrepository = IAppUserrepository;
        }
        public async Task AddUserToApp(UserToAppAddCommandViewModel model)
        {
           await _IAppUserrepository.AddUserToApp(model);
          await  _unitOfWork.SaveAsync();
        }
    }
}
