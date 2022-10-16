using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.AppAggregate
{
    public class RemoveAppBaseViewModelModel
    {
        public int AppId { get; set; }
        public string Title { get; set; }
        public string message { get; set; }

        public static explicit operator RemoveAppBaseViewModelModel(AppEntity v)
        {
            return new RemoveAppBaseViewModelModel()
            {
                Title = v.Title,
                AppId = v.Id
            };
        }
    }
}
