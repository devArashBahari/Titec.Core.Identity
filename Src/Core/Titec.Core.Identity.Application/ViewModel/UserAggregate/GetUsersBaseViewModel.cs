using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.Tools.Helper;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class GetUsersBaseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string rowLevelAccess { get; set; }
        public bool IsActive { get; set; }


        public static explicit operator GetUsersBaseViewModel(UserEntity v)
        {
            if (v == null)
            {
                return null;
            }
            return new GetUsersBaseViewModel()
            {
                MobileNo = v.MobileNo,
                FirstName = v.FirstName,
                LastName = v.LastName,
                Email = v.Email,
                UserName = v.UserName,
                Id = v.Id,
                rowLevelAccess = v.rowLevelAccess.ToString(),
                IsActive = v.IsActive
            };
        }

        //public static explicit operator GetUsersBaseViewModel(PagedResult<UserEntity> v)
        //{
         
        //}
    }
}
