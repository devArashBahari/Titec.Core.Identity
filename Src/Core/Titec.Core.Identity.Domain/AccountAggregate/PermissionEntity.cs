namespace Titec.Core.Identity.Domain.AccountAggregate
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }

        public string Path { get; set; }
        public byte MethodId { get; set; }
        //public string MethodTitle { get; set; }

        public bool isDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastEditeDate { get; set; }
        public int Creator { get; set; }
        public int? LastEditor { get; set; }

        public int? ParentId { get; set; }

        public PermissionEntity Permission { get; set; }
        public ICollection<PermissionEntity> Permissions { get; set; }

        public ICollection<RolePermissionEntity> RolePermissions { get; set; }
        public int AppId { get; set; }
        public AppEntity App { get; set; }


    }
}
