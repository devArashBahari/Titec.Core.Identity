using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.PermissionAggregate
{
    public class PermissionBaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }

        public static explicit operator PermissionBaseViewModel(PermissionEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new PermissionBaseViewModel()
            {
               Id = entity.Id,
               Title = entity.Title,
               ParentId = entity.ParentId,
            };
        }

        public static explicit operator PermissionBaseViewModel(RolePermissionEntity entity)
        {
             if (entity == null)
            {
                return null;
            }
            return new PermissionBaseViewModel()
            {
                Title = entity.Permission.Title,
                Id = (int)entity.PermissionId,
                ParentId=entity.Permission.ParentId

            };
        }
    }
}
