using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Service.ServiceAggregate.UserCustomerAggregate
{
    public class UserCustomerService: IUserCustomerService
    {
        private readonly IUserCustomerRepository _UserCustomerrepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserCustomerService(IUserCustomerRepository UserCustomerRepository, IUnitOfWork unitOfWork)
        {
            _UserCustomerrepository = UserCustomerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddCustomersToUser(AddCustomersToUserCommandViewModel model)
        {
            await _UserCustomerrepository.AddCustomersToUser(model);
            await _unitOfWork.SaveAsync();
        }
    }
}
