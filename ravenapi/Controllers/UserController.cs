﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ravenapi.Services;
using ravenapi.Entities;
//using System.Security.Claims;

namespace ravenapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            
            return Ok(user);
        }

        /*
        [HttpGet]
        public IActionResult GetAll()
        {
            var userName = User.Identity.Name;

            var users = _userService.GetAll();

            return Ok(userName);
        }
        */

        
    }
}