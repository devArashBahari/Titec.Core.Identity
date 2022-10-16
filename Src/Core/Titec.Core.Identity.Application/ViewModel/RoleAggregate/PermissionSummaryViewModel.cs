namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class PermissionSummaryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Path { get; set; }
        public byte MethodId { get; set; }
        //public string MethodTitle => Enum.GetName(typeof(HttpMethod), MethodId);
        public ICollection<PermissionSummaryViewModel> SubPermissions { get; set; }
    }
}
