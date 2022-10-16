using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ServiceContract;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private IPermissionService _PermissionService;

        public PermissionController(IPermissionService PermissionService)
        {

            _PermissionService = PermissionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var res = await _PermissionService.GetPermissions();
            return Ok(res);
        }
    }
}
