using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class RoleBaseViewModel
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AppId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public static explicit operator RoleBaseViewModel(RoleEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new RoleBaseViewModel()
            {
                Alias = entity.Alias,
                Description = entity.Description,
                Id = entity.Id,
                Name = entity.Name,
                IsAdmin = entity.IsAdmin,
                AppId = entity.AppId,
                IsSuperAdmin = entity.IsSuperAdmin,

            };
        }
    }
}
