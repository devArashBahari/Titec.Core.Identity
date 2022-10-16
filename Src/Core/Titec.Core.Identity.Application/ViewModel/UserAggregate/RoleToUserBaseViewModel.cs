using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class RoleToUserBaseViewModel
    {
        public List<int> roleIds { get; set; }
        public int userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }

        public static explicit operator RoleToUserBaseViewModel(UserEntity v)
        {
            if (v == null)
            {
                return null;
            }
            return new RoleToUserBaseViewModel()
            {
                MobileNo = v.MobileNo,
                FirstName = v.FirstName,
                LastName = v.LastName,
                Email = v.Email,
                Password = v.Password,
                UserName = v.UserName,
                userId = v.Id,

            };
        }
    }
}
