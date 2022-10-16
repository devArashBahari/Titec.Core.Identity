using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class UserForRoleBaseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }

        public static explicit operator UserForRoleBaseViewModel(UserEntity? entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new UserForRoleBaseViewModel()
            {

                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                UserName = entity.UserName,
                MobileNo = entity.MobileNo,
            };
        }

      
    }
}
