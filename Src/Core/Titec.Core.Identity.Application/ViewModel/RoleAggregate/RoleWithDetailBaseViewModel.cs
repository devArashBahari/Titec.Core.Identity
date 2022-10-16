using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;

namespace Titec.Core.Identity.Application.ViewModel.RoleAggregate
{
    public class RoleWithDetailBaseViewModel
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public bool IsAdmin { get; set; }

        public string massege { get; set; }

        public List<PermissionSummaryBaseViewModel> Permissions { get; set; }
    }
}
