using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.IdentityAggregate
{
    public class LoginWithOtpAddCommandModel
    {
        public string MobileNumber { get; set; }
        public string OtpCode { get; set; }
    }
}
