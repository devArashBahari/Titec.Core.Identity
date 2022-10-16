using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.PermissionAggregate
{
    public class PermissionSummaryBaseViewModel
    {
        public string Title { get; set; }
        public string Alias { get; set; }

     

        public static explicit operator PermissionSummaryBaseViewModel(PermissionEntity v)
        {
            return new PermissionSummaryBaseViewModel()
            {
                Alias = v.Alias,
                Title = v.Title
            };
        }
        //public string Path { get; set; }
        //public byte MethodId { get; set; }
        //public string MethodTitle { get; set; }
    }
}
