using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using HealthTrackr_Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrackr_Api.Controllers
{
	[ApiController]
	[Route("/api")]
	public class LoginController : ControllerBase
	{
		private readonly ILogger<LoginController> logger;
		private readonly AccessRepository accessRepository;
		private readonly LoginAccess loginAccess;

		public LoginController(ILogger<LoginController> logger, AccessRepository accessRepository, LoginAccess loginAccess)
		{
			this.logger = logger;
			this.accessRepository = accessRepository;
			this.loginAccess = loginAccess;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel login)
		{
			try
			{
				if (!loginAccess.CheckAuthAccess(Request.Headers["Authorization"].ToString() ?? string.Empty))
				{
					return Unauthorized();
				}

				var result = await accessRepository.UserLogin(login);
				return Ok(result);
			}
			catch (Exception)
			{
				throw new Exception();
			}
		}

		[HttpPost]
		[Route("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody] LoginModel login)
		{
			try
			{
				if (!loginAccess.CheckAuthAccess(Request.Headers["Authorization"].ToString() ?? string.Empty))
				{
					return Unauthorized();
				}

				var result = await accessRepository.ChangePassword(login);
				return Ok(result);
			}
			catch (Exception)
			{
				throw new Exception();
			}
		}
	}
}
