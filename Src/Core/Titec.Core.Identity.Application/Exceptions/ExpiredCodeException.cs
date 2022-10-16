using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.Exceptions
{
    public class ExpiredCodeException:Exception
    {
        public ExpiredCodeException()
        {

        }
        public ExpiredCodeException(string msg) : base(msg)
        {

        }
    }
}
