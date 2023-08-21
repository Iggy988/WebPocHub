﻿using Microsoft.AspNetCore.Mvc;
using Models;
using WebPocHub.Dal;
using WebPocHub.Models;


namespace WebPocHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WphAuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _wphAuthentication;
        //private readonly ITokenManager _tokenManager;
        public WphAuthenticationController(IAuthenticationRepository wphAuthentication /*,ITokenManager tokenManager*/)
        {
            _wphAuthentication = wphAuthentication;
            //_tokenManager = tokenManager;
        }

        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(User user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            var result = _wphAuthentication.RegisterUser(user);
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("CheckCredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthResponse> GetDetails(User user)
        {
            var authUser = _wphAuthentication.CheckCredentials(user);
            if (authUser == null)
            {
                return NotFound();
            }
            if (authUser != null && !BCrypt.Net.BCrypt.Verify(user.Password, authUser.Password))
            {
                return BadRequest("Incorrect Password! Please check your password!");
            }
            //var roleName = _wphAuthentication.GetUserRole(authUser.RoleId);
            var authResponse = new AuthResponse() { IsAuthenticated = true, Role = "Dummy Role", Token = "Dummy Token" };
            return Ok(authResponse);
        }
    }
}
