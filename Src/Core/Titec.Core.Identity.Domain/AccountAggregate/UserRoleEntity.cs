using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class UserRoleEntity: AuditEntity<Guid>
    {

        public int UserId { get; set; }

        public int RoleId { get; set; }


        public UserEntity User { get; set; }

        public RoleEntity Role { get; set; }
    }
}
