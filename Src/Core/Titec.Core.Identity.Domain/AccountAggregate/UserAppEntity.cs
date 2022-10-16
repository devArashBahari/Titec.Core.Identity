using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class UserAppEntity : AuditEntity<int>
    {
        public int UserId { get; set; }

        public int AppId { get; set; }


        public UserEntity User { get; set; }

        public AppEntity App { get; set; }
    }
}
