using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.UserAppAggregate
{
    public class UserAppRepository : GenericRepository<UserAppEntity, int>, IAppUserRepository
    {
        public UserAppRepository(BaseDbContext dbContext, ITitecIdentity currentIdentity) : base(dbContext, currentIdentity)
        {
        }

        public async Task AddUserToApp(UserToAppAddCommandViewModel model)
        {
            foreach (var item in model.UserIds)
            {
                await base.AddAsync(new UserAppEntity()
                {
                    AppId = model.AppId ,
                    UserId = item
                });
            }
        }
    }
}