using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class UpdateRolesOfUserAddCommandModel
    {
        public List<int> roleIds { get; set; }
    }
}
