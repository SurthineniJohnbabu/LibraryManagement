using LibraryManagement.API.BusinessLogic.Users;
using LibraryManagement.API.Core.Constants;
using LibraryManagement.API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.API.Controllers.Login
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServiceManager userService;

        public UserController(IUserServiceManager userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Post([FromBody]LoginDetails login)
        {
            try
            {
                var user = await userService.Authenticate(login.UserName, login.Password);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> Get()
        {
            try
            {
                if(!User.IsInRole(Role.Admin))
                    return Forbid();

                var users = await userService.GetAllUsers();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> Get(int userId)
        {
            try
            {
                var user = await userService.GetUserById(userId);
                if (user == null)
                    return NotFound();

                // only allow admins to access other user records
                var currentUserId = int.Parse(User.Identity.Name);
                if (userId != currentUserId && !User.IsInRole(Role.Admin))
                    return Forbid();

                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}