using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.UserCustomerAggregate
{
    public class UserCustomerRepository : GenericRepository<UserCustomerEntity, int>, IUserCustomerRepository
    {
        public UserCustomerRepository(BaseDbContext dbContext, ITitecIdentity currentIdentity) : base(dbContext, currentIdentity)
        {
        }

        public async Task AddCustomersToUser(AddCustomersToUserCommandViewModel model)
        {

            base._dbSet.Where(r => r.UserId == model.UserId).ToList().ForEach(r => base._dbSet.Remove(r));
            var CustomerUser = new AddCustomersToUserCommandViewModel()
            {
                UserId = model.UserId,
                CustomerIds = model.CustomerIds,
            };
            foreach (var item in CustomerUser.CustomerIds)
            {
                await base.AddAsync(new UserCustomerEntity()
                {
                    UserId = CustomerUser.UserId,
                    CustumerId = item
                });
            }
        }
    }
}
