using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class UpdatePermissionsOfRoleAddCommandModel
    {
        public int RoleID { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
