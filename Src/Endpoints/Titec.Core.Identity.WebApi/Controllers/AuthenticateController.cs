using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController :  ControllerBase
    {
        private IUserService userService;

        public AuthenticateController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAddCommandModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUser(login);
                if (result == null)
                {
                    return NotFound("کاربری با این مشخصات موجود نیست");
                }
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
