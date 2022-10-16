using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.IdentityAggregate
{
    public class RegisterBaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }

        public static explicit operator RegisterBaseViewModel(UserEntity v)
        {
            return new RegisterBaseViewModel()
            {
                FirstName = v.FirstName,
                LastName = v.LastName,
                Email = v.Email,
                UserName = v.UserName,
                MobileNo = v.MobileNo,
            };
        }
    }
}