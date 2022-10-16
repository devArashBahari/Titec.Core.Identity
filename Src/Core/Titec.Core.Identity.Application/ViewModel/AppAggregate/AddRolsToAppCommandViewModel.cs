namespace Titec.Core.Identity.Application.ViewModel.AppAggregate
{
    public class AddRolsToAppCommandViewModel
    {
        public List<int> RolsIDs { get; set; }
        public int AppId { get; set; }
    }
}
