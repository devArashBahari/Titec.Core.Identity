using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.AppAggregate;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(AppCommandModel model)
        {
            var app = await _appService.AddAsync(model);
            return Ok(app);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromQuery] int AppId, AppCommandModel model)
        {
            var app = await _appService.EditAsync(model, AppId);
            return Ok(app);
        }

        [HttpPost("RemoveById")]
        public async Task<IActionResult> RemoveByIdAsync([FromQuery] int appId)
        {
            await _appService.RemoveByIdAsync(appId);
            return NoContent();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var apps = await _appService.GetAllAsync();
            return Ok(apps);
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int appId)
        {
            var app = await _appService.GetByIdAsync(appId);
            return Ok(app);
        }




        //[HttpPost("AddUsersToApp")]
        //public async Task<IActionResult> AddUsersToApp(UserToAppAddCommandViewModel model)
        //{
        //    await _appService.AddUserToApp(model);
        //    return Ok();
        //}
        ////[HttpPost("AddPermissionsToApp")]
        ////public async Task<IActionResult> AddPermissionsToApp(AddPermissionsToAppCommandViewModel model)
        ////{
        ////   await _appService.AddPermissionsToApp(model);
        ////    return Ok();
        ////}
        ////[HttpPost("AddRolsToApp")]
        ////public async Task<IActionResult> AddRolsToApp(AddRolsToAppCommandViewModel model)
        ////{
        ////   await _appService.AddRolsToApp(model);
        ////    return Ok();
        ////}
        //[HttpGet("GetRollesAndPermissions")]
        //public async Task<IActionResult> GetRollesAndPermissionsOfApp([FromQuery] string AppName)
        //{
        //    var result = await _appService.GetRollesAndPermissionsOfApp(AppName);

        //    foreach (var item in result)
        //    {
        //        if (item.massege != null)
        //        {
        //            return BadRequest(item.massege);
        //        }
        //    }
        //    return Ok(result);
        //}

    }
}
