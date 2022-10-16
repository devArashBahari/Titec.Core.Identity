using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.IdentityAggregate
{
    public class SendSMSAddViewModel
    {
        public List< string> Receptors { get; set; }
        public string Message { get; set; }
        public int Operator { get; set; } = 1;
    }
}
