

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.Tools.Helper;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.IdentityAggregate
{
    public class RegisterAddCommandModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد .")]
        public string MobileNo { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastName { get; set; }
        [Display(Name = "شناسه مشتری ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsActive { get; set; }
    
        public RowLevelAccess rowLevelAccess { get; set; }

        public static explicit operator UserEntity(RegisterAddCommandModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new UserEntity()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNo = model.MobileNo,
                UserName = model.UserName,
                Password = model.Password,
                CustomerId = model.CustomerId,
                rowLevelAccess =model.rowLevelAccess,
                IsActive = model.IsActive,
                CustomerName=model.CustomerName
            };
        }
    }
}










