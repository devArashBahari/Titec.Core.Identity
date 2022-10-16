using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class AppEntity : AuditEntity<int>
    {
        public string Title { get; set; }
        public string Name { get; set; }

        public ICollection<UserAppEntity> UserApps { get; set; }
        public ICollection<PermissionEntity> Permissions{ get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
    }
}
