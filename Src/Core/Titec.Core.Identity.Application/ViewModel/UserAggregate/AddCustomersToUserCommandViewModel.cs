using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class AddCustomersToUserCommandViewModel
    {
        public List<int> CustomerIds { get; set; }
        public int UserId { get; set; }
    }
}
