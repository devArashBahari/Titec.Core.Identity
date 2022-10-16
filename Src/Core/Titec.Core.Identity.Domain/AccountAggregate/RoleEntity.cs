using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class RoleEntity : AuditEntity<int>
    {
        public string? Description { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public List<RolePermissionEntity> RolePermissions { get; set; }
        public int AppId { get; set; }
        public AppEntity App { get; set; }
    }
}
