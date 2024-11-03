using Asp.Versioning;
using HealthTrackr_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrackr_Api.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("/api/v{v:apiVersion}/user")]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserServices _userServices;

        public UserController(ILogger<UserController> logger, UserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Route("getUser")]
        //[FeatureGate("IsActive")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var result = await _userServices.GetUserInformation();
                return Ok(result);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
