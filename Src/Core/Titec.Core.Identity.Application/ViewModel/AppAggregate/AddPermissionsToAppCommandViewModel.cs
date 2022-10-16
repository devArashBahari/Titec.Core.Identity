namespace Titec.Core.Identity.Application.ViewModel.AppAggregate
{
    public class AddPermissionsToAppCommandViewModel
    {
        public List<int> PermissionIDs { get; set; }
        public int AppId { get; set; }
    }
}
