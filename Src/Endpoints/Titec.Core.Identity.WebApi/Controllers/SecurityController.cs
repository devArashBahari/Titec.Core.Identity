
using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;
using Titec.Core.Identity.Application.ServiceContract;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IRoleService roleService;

        public SecurityController( IRoleService roleService)
        {
            this.roleService = roleService;
        }
       

        [HttpGet("GetAppRoleAndPermissions/{appName}")]
        public async Task<IActionResult> GetRolePermissionsByAppNameAsync(string appName)
        {
            var result = await roleService.GetAllWithDetails(appName);
            return Ok(result);
        }
    }
}
