using HRHelper.Application.Interface;
using HRHelper.Application.ViewModels;
using HRHelper.Login.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;


        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        #region API/Users

        [HttpGet, AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(this.userService.Get());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult GetById(string id)
        {
            return Ok(this.userService.GetById(id));
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Post(UserViewModel userViewModel)
        {
            return Ok(this.userService.Post(userViewModel));
        }

        [HttpPut]
        public IActionResult Update(UserViewModel userViewModel)
        {
            return Ok(this.userService.Update(userViewModel));
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            string _userId = TokenService.GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);
            return Ok(this.userService.Delete(_userId));
        }
        #endregion

        [HttpPost("login"), AllowAnonymous]
        public IActionResult Login(UserAuthenticateRequestViewModel userViewModel)
        {
            return Ok(this.userService.Login(userViewModel));
        }

    }
}
