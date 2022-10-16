namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class RoleDetailWithPermissions
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Alias { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSuperAdmin { get; set; }

        public int AppId { get; set; }
        public IEnumerable<PermissionSummaryViewModel> Permissions { get; set; }
    }
}
