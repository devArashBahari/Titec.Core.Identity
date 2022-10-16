using System.ComponentModel.DataAnnotations;
using Titec.Framework.Domain.BaseEntities;

namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class UserEntity : AuditEntity<int>
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(11)]
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(6),MinLength(6)]
        public string? OtpCode { get; set; }
        [MaxLength(100)]
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }

        public DateTime? OtpCodeExpiry { get; set; }
        public RowLevelAccess rowLevelAccess { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
        public ICollection<UserAppEntity> userApps { get; set; }
        public ICollection<UserCustomerEntity> userCustomers { get; set; }


    }
    public enum RowLevelAccess:short
    {
        [Display(Name = "ادمین")]
        Admin = 1,
        [Display(Name = "مدیر")]
        Manager = 2,
        [Display(Name = "کاربر")]
        User =3
    }
}
