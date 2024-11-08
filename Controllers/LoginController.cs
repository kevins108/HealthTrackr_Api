﻿using Asp.Versioning;
using HealthTrackr_Api.Models;
using HealthTrackr_Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrackr_Api.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("/api/v{v:apiVersion}/")]

    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly AccessRepository accessRepository;

        public LoginController(ILogger<LoginController> logger, AccessRepository accessRepository)
        {
            this.logger = logger;
            this.accessRepository = accessRepository;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await accessRepository.ValidateCredentials(login);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = accessRepository.GenerateToken(user);
            return Ok(token);
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("createaccount")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] AccountModel account)
        {
            var user = await accessRepository.CreateUserAccount(account);
            if (user)
            {
                return Ok("Account created successfully");
            }
            return BadRequest("Account creation failed");
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("changepassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel login)
        {
            try
            {
                var result = await accessRepository.ChangePassword(login);
                if (result)
                {
                    return Ok("Password changed");
                }
                return BadRequest("Password change failed");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
