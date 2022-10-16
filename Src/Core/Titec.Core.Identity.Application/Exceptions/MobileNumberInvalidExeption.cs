using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.Exceptions
{
    public class MobileNumberInvalidExeption : Exception
    {
        public MobileNumberInvalidExeption()
        {

        }
        public MobileNumberInvalidExeption(string msg) : base(msg)
        {

        }
    }
}
