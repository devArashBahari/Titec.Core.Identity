using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.AppAggregate
{
    public class AppBaseViewModel
    {
        public int AppId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }

        public static explicit operator AppBaseViewModel(AppEntity entity)
        {
            return new AppBaseViewModel()
            {
                Name =  entity.Name,
                Title = entity.Title,
                AppId = entity.Id
            };
        }
    }
}
