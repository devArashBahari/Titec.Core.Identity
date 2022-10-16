using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.ViewModel.IdentityAggregate
{
    public class GenerateOtpBaseViewModel
    {
        public string OTP { get; set; }

        public static explicit operator GenerateOtpBaseViewModel(string v)
        {
            return new GenerateOtpBaseViewModel()
            {
                OTP = v
            };
        }
    }
}
