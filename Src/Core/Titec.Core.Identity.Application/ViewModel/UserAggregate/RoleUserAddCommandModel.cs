using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class RoleUserAddCommandModel
    {
        public List<int> roleIds { get; set; }
        public int userId { get; set; }

        public static explicit operator RoleUserAddCommandModel(EditUserRoleAddCommandModelViewModel v)
        {
            if (v==null)
            {
                return null;
            }
            return new RoleUserAddCommandModel()
            {
                roleIds = v.roleIds,
                userId = v.userId
            };
        }
    }
}
