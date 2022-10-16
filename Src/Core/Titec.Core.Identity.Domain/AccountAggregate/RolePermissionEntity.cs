using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class RolePermissionEntity : AuditEntity<int>
    {

        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public RoleEntity Role { get; set; }
        public PermissionEntity Permission { get; set; }
    }
}
