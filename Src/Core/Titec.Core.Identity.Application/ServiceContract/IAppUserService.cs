using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;

namespace Titec.Core.Identity.Application.ServiceContract
{
    public interface IAppUserService
    {
        Task AddUserToApp(UserToAppAddCommandViewModel model);
    }
}
