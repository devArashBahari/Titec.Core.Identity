using System.ComponentModel.DataAnnotations;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;

namespace Titec.Core.Identity.Application.ViewModel.PermissionAggregate
{
    public class PermissionRoleAddCommandViewModel
    {
        [Required(ErrorMessage = "پر کردن این فیلد الزامی است")]
        public int roleId { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامی است")]
        public List<int> permissionId { get; set; }

        public static explicit operator PermissionRoleAddCommandViewModel(UpdatePermissionsOfRoleAddCommandModel v)
        {
            if (v==null)
            {
                return null;
            }
            return new PermissionRoleAddCommandViewModel()
            {
                permissionId = v.PermissionIds,
                roleId = v.RoleID
            };
        }
    }
}
