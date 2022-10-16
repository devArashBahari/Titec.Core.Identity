using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class UsersWithRolsBaseViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> RoleName { get; set; }
    }
}
