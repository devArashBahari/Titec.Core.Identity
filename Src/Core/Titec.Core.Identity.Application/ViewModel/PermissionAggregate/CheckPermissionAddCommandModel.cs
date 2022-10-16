using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.PermissionAggregate
{
    public class CheckPermissionAddCommandModel
    {
        public int permissionId { get; set; }
        public string userName { get; set; }
    }
}
