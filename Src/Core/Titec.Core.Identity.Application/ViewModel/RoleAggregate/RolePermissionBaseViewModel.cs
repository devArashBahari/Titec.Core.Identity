using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class RolePermissionBaseViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public List<PermissionBaseViewModel> PermissionTitle { get; set; }
    }
}
