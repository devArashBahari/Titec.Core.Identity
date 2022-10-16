using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class RoleCommandModel
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AppId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public static explicit operator RoleEntity(RoleCommandModel model)
        {
            if (model == null) return null;

            return new RoleEntity()
            {
                Name = model.Name,
                Alias = model.Alias,
                Description = model.Description,
                AppId = model.AppId,
                IsAdmin = model.IsAdmin,
                IsSuperAdmin = model.IsSuperAdmin,
            };
        }
    }
}
