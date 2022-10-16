using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ViewModel.PermissionAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;

using Titec.Core.Identity.Application.ServiceContract;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private IRoleService _RoleService;

        public RoleController(IRoleService roleService)
        {

            _RoleService = roleService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] RoleCommandModel command)
        {
            var Role = await _RoleService.AddAsync(command);
            return Ok(Role);
        }
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync(RoleCommandModel command, [FromQuery] int roleId)
        {
            var Rols = await _RoleService.EditAsync(command, roleId);
            return Ok(Rols);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _RoleService.GetAllAsync();
            return Ok(res);
        }

        [HttpPost("AddRoleAndPermissions")]
        public async Task<IActionResult> AddRoleAndPermissions(PermissionRoleAddCommandViewModel role)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            //Add Roles
            await _RoleService.AddRoleAndPermissions(role);
            return Ok();

        }
        //   [HttpPost("UpdateRolePermissions")]
        //   public async Task<IActionResult> UpdateRolePermissions([FromBody] UpdateRolePermissionAddCommandModel command
        //)
        //   {

        //       var Role = await _RoleService.UpdateRolePermissions(command);
        //       if (Role.Massege != null)
        //       {
        //           return NotFound(Role.Massege);
        //       }
        //       return Ok(Role);
        //   }
        [HttpPost("UpdatePermissionsOfRole")]
        public async Task<IActionResult> UpdatePermissionsOfRole(UpdatePermissionsOfRoleAddCommandModel command
)
        {
            await _RoleService.UpdateRolePermissions(command);
            return Ok();
        }
        [HttpDelete("RemoveById")]
        public async Task<IActionResult> RemoveByIdAsync([FromQuery] int id)
        {
            await _RoleService.RemoveByIdAsync(id);
            return NoContent();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int appId)
        {
            var app = await _RoleService.GetByIdAsync(appId);
            return Ok(app);
        }

        [HttpPost("UpdateRolesOfUser")]
        public async Task<IActionResult> UpdateRolesOfUser([FromQuery] int UserId, UpdateRolesOfUserAddCommandModel roleToUser)
        {
            await _RoleService.EditRolesUser(UserId, roleToUser);
            return Ok();
        }
        [HttpPost("GetRolAndPermissionById")]
        public async Task<IActionResult> GetRolAndPermissionById([FromQuery] int roleId)
        {
            var res = await _RoleService.GetRolAndPermissionById(roleId);
     
            return Ok(res);
        }

    }
}
