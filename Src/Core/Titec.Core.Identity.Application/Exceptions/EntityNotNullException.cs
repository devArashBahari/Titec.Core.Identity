using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Application.Exceptions
{
    public class EntityNotNullException: Exception
    {
        public EntityNotNullException()
        {

        }
        public EntityNotNullException(string msg) : base(msg)
        {

        }
    }
}
