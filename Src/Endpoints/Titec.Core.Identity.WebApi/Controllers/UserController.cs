
using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }
        //[HttpPost("AddUserRoles")]
        //public async Task<IActionResult> AddUserRoles(RoleUserAddCommandModel model)
        //{
        //    await _UserService.AddRoleToUser(model);
        //    return Ok();
        //}
        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers(GetUserAddCommandModel model)
        {

            //var rowLevelNumbers = new List<string>();

            var Users = await _UserService.FilterSortPagination(model);
            //foreach (var item in Users)
            //{
            //    rowLevelNumbers.Add(item.rowLevelAccess.ToString());

            //    foreach (var item1 in rowLevelNumbers)
            //    {
            //        var rowlevel =
            //             (RowLevelAccess)Enum.Parse(typeof(RowLevelAccess), item1);
            //        item.rowLevelAccess = rowlevel.ToString();
            //    }

            //}


            return Ok(Users);
        }
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(EditUserCommandModel model, [FromQuery] int userId)
        {
            var user = await _UserService.EditUser(model, userId);
            return Ok(user);
        }
        [HttpPost("AddOrUpdateCustomersOfUser")]
        public async Task<IActionResult> AddOrUpdateCustomersOfUser(AddCustomersToUserCommandViewModel model)
        {
            await _UserService.UpdateCustomersToUser(model);
            return Ok();
        }
        [HttpGet("GetUsersWithRols")]
        public async Task<IActionResult> GetUsersWithRols()
        {
           var res= await _UserService.GetUsersWithRols();
            return Ok(res);
        }
        [HttpGet("GetRowLevelAccessList")]
        public async Task<IActionResult> GetRowLevelAccessList()
        {
            var res = await _UserService.GetRowLevelAccessList();
            return Ok(res);
        }


    }
}
