using HealthTrackr_Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrackr_Api.Controllers
{
	[ApiController]
	[Route("/api/user")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly UserRepository _userRepository;

		public UserController(ILogger<UserController> logger, UserRepository userRepository)
		{
			_logger = logger;
			_userRepository = userRepository;
		}

		[HttpGet]
		[Route("getUser")]
		//[FeatureGate("IsActive")]
		public async Task<IActionResult> GetUser()
		{
			try
			{
				var result = await _userRepository.GetUserInformation();
				return Ok(result);
			}
			catch (Exception)
			{
				throw new Exception();
			}

		}
	}
}
