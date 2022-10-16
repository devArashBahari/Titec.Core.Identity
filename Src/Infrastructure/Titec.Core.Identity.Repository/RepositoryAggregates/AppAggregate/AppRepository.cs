using Microsoft.EntityFrameworkCore;
using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.AppAggregate
{
    public class AppRepository : GenericRepository<AppEntity, int>, IAppRepository
    {
        public AppRepository(BaseDbContext dbContext, ITitecIdentity currentIdentity) : base(dbContext, currentIdentity)
        {
        }

        //public Task AddPermissionsToApp(AddPermissionsToAppCommandViewModel model)
        //{
        //    foreach (var item in model.PermissionIDs)
        //    {
        //        await base.AddAsync(new AppEntity()
        //        {
        //            AppId = model.AppId,
        //             = item
        //        });
        //    }
        //}

        //public Task AddRolsToApp(AddRolsToAppCommandViewModel model)
        //{
        //    foreach (var item in model.UserId)
        //    {
        //        await base.AddAsync(new UserAppEntity()
        //        {
        //            AppId = model.AppId,
        //            UserId = item
        //        });
        //    }
        //}

        public async Task<AppEntity> EditAsync(AppEntity entity)
        {
            await base.EditAsync(entity);
            return entity;
        }

        public async Task<IList<AppBaseViewModel>> GetAllAsync()
        {
            var app = base._dbSet.Where(p => p.IsDeleted == false).Select(p => (AppBaseViewModel)p).ToList();
            return app;
        }

        public async Task<AppBaseViewModel> GetByIdAsync(int appId)
        {
            var app = await base._dbSet.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == appId);
            if (app == null)
            {
                throw new EntityNotFoundException("اطلاعات یافت نشد");
            }
            return (AppBaseViewModel)app;
        }

        //public async Task<List<RoleWithDetailBaseViewModel>> GetRollesAndPermissionsOfApp(string AppName)
        //{
        //    var app = await _dbSet.Include(x => x.Roles).ThenInclude(x => x.RolePermissions).ThenInclude(x => x.Permission).Where(x => x.Title == AppName).ToListAsync();
        //    if (app.Count == 0)
        //    {
        //        var message = new List<RoleWithDetailBaseViewModel>();
        //        message.Add(new RoleWithDetailBaseViewModel()
        //        {
        //            massege = "اپلیکیشن موجود نمی باشد"
        //        });
        //        return message;

        //    }

        //    List<RoleWithDetailBaseViewModel> roles = new List<RoleWithDetailBaseViewModel>();

        //    foreach (var item in app.SelectMany(x => x.Roles))
        //    {
        //        List<PermissionSummaryBaseViewModel> permissions = new List<PermissionSummaryBaseViewModel>();
        //        foreach (var item2 in item.RolePermissions.Where(x => x.RoleId == item.Id).Select(x => x.Permission))
        //        {
        //            permissions.Add(new PermissionSummaryBaseViewModel()
        //            {
        //                Alias = item2.Alias,
        //                Title = item2.Title
        //            });
        //        }
        //        roles.Add(new RoleWithDetailBaseViewModel
        //        {
        //            Alias = item.Alias,
        //            IsAdmin = false,
        //            Title = item.Name,
        //            Permissions = permissions

        //        });



        //    }


        //    //var app=await base._dbSet.FirstOrDefaultAsync(x=>x.Title == AppName);



        //    //var roleapp = await base._dbSet.Include(x => x.RoleEntities).SelectMany(x => x.RoleEntities).Where(x => x.AppId == app.Id).ToListAsync();

        //    //var RolePermission = new List<int>();

        //    //foreach (var item in roleapp)
        //    //{
        //    // var mm=   item.RolePermissions.Where(x => x.RoleId == app.Id).Select(x=>x.PermissionId).ToList();
        //    //    RolePermission.AddRange((IEnumerable<int>)mm);
        //    //}
        //    //foreach (var item in RolePermission)
        //    //{
        //    //    await base._dbSet.Include(x=>x.RoleEntities).ThenInclude(x=>x.RolePermissions).ThenInclude(x=>x.Permission).Where(x=>x.permissionEntities.)
        //    //}



        //    return roles;

        //}

        public async Task<bool> IsAppExist(string appName)
        {
            return await base._dbSet.AnyAsync(x => x.Name == appName);
        }

        Task<List<RoleWithDetailBaseViewModel>> IAppRepository.GetRollesAndPermissionsOfApp(string AppName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveByIdAsync(int AppId)
        {
            var app = await base._dbSet.FirstOrDefaultAsync(x => x.Id == AppId);

            if (app == null)
            {
                throw new EntityNotFoundException("اطلاعات یافت نشد");
            }

            app.IsDeleted = true;
            await base.EditAsync(app);
        }

        public async Task<AppEntity> AddAsync(AppEntity entity)
        {
          await base.AddAsync(entity);
            return entity;
        }

        public async Task<AppEntity> GetEntityById(int appId)
        {
            return await base._dbSet.FirstOrDefaultAsync(x => x.Id == appId);
        }
    }
}
