
using Microsoft.AspNetCore.Mvc;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;
using Titec.Core.Identity.Application.ServiceContract;

namespace Titec.Core.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAddCommandModel register)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.RegisterUser(register);
                return Ok(res);
            }

            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAddCommandModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUser(login);
                return Ok(result);
            }

            return BadRequest();
        }
        //[HttpGet("Sign-Out")]
        //public async Task<IActionResult> LogOut()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await HttpContext.SignOutAsync();
        //        return Ok("خروج با موفقیت انجام شد");
        //    }

        //    return BadRequest("مشکلی در خروج به وجود امده است");
        //}

        [HttpPost("GenerateOtp")]
        public async Task<IActionResult> GenerateOtp([FromBody] GenerateOtpAddCommandModel model)
        {
            await userService.GenerateOTP(model);
            return Ok();
        }
        [HttpPost("LoginWithOtp")]
        public async Task<IActionResult> LoginWithOtp([FromBody] LoginWithOtpAddCommandModel model)
        {
            return Ok(await userService.LoginWithOtp(model));
        }
    }
}
