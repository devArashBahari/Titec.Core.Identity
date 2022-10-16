using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class UserCustomerEntity : AuditEntity<int>
    {
        public int UserId { get; set; }
        public int CustumerId { get; set; }

      public UserEntity user { get; set; }
    
    }
}
