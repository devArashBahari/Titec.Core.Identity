using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titec.Core.Identity.Service.Validations
{
    public static class ValidationHelper
    {
        public static bool CheckMobileNo(this string mobileNo)
        {
            if (string.IsNullOrEmpty(mobileNo))
            {
                return false;
            }

            if (mobileNo.Trim().Length < 11)
            {
                return false;
            }

            Regex rgx = new(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
            return rgx.IsMatch(mobileNo);
        }
        public static bool CheckPassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Trim().Length < 8)
            {
                return false;
            }
            List<string> list = new List<string>();
            list.Add("!");
            list.Add("@");
            list.Add("#");
            list.Add("$");
            list.Add("%");
            list.Add("^");
            list.Add("&");
            list.Add("*");
            foreach (var item in list)
            {
                if (password.Contains(item))
                {
                    return true;
                }
            }
            Regex rgx = new(@"(^(09|9)[1][1-9]\d{7}$)|(^(09|9)[3][12456]\d{7}$)");
            if (rgx.IsMatch(password))
            {
                return true;
            }
            return false ;
        }
        public static bool CheckAlliesEnglish(this string Allies)
        {
            if (string.IsNullOrEmpty(Allies))
            {
                return false;
            }
            Regex rgx = new(@"^[A-Za-z0-9]*$");
            if (rgx.IsMatch(Allies))
            {
                return true;
            }
            return false;
        }
    }
}
