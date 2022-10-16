using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.Exceptions
{
    public class InvalidPasswordExeption : Exception
    {
        public InvalidPasswordExeption()
        {

        }
        public InvalidPasswordExeption(string msg) : base(msg)
        {

        }
    }
}
