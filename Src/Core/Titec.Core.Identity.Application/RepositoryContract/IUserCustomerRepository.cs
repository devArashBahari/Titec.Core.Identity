using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;

namespace Titec.Core.Identity.Application.RepositoryContract
{
    public interface IUserCustomerRepository
    {
        Task AddCustomersToUser(AddCustomersToUserCommandViewModel model);
    }
}
