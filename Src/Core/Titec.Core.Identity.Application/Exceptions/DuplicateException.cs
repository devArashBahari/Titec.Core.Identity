namespace Titec.Core.Identity.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException()
        {

        }
        public DuplicateException(string msg) : base(msg)
        {

        }
    }
}
