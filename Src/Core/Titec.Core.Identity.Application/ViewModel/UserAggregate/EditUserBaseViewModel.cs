using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.Application.ViewModel.UserAggregate
{
    public class EditUserBaseViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }

        public static explicit operator EditUserBaseViewModel(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new EditUserBaseViewModel()
            {
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                MobileNo = entity.MobileNo,
                UserId = entity.Id
            };
        }
    }
}
