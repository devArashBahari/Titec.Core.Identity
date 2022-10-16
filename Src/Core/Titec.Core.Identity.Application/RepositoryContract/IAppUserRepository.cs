using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IAppUserRepository
    {
        Task AddUserToApp(UserToAppAddCommandViewModel model);
    }
}
