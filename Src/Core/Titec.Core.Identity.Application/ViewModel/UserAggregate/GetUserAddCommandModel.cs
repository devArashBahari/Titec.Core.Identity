namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class GetUserAddCommandModel
    {
        public string? Orderfiled { get; set; } = "FirstName";
        public string? SearchText { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 4;
        public string? sortDirection { get; set; } = "asc";

 
    }
}
