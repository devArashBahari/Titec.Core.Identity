using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.AppAggregate
{
    public class AppCommandModel
    {
        public string Title { get; set; }
        public string Name { get; set; }

        public static explicit operator AppEntity(AppCommandModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new AppEntity()
            {
                Title = model.Title,
                Name = model.Name
            };
        }
    }
}
