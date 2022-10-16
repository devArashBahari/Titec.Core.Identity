//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
//using Titec.Core.Identity.Application.ServiceContract;

//namespace Titec.Core.Identity.Common.Security
//{
//    public class PermissionCheckerAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute, IAuthorizationFilter
//    {
//        private IPermissionService _permissionService;
//        private int _permissionId= 0 ;

//        public PermissionCheckerAttribute(int permissionId)
//        {
//            _permissionId = permissionId;
//        }

//        public async void OnAuthorization(AuthorizationFilterContext context)
//        {
//            _permissionService =
//           (IPermissionService) context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
//            if ( context.HttpContext.User.Identity.IsAuthenticated)
//            {
//                string userName = context.HttpContext.User.Identity.Name;
//                var check = new CheckPermissionAddCommandModel()
//                {
//                    permissionId = _permissionId,
//                    userName = userName,
//                };

//                if (!_permissionService.CheckPermission(check))
//                {
//                    context.Result = new ForbidResult();
//                }
//            }
//            else
//            {
//                context.Result = new ForbidResult();
//            }
//        }
//    }
//}
