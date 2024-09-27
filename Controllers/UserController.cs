using HealthTrackr_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrackr_Api.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly UserServices userServices;

        public UserController(ILogger<UserController> logger, UserServices userServices)
        {
            this.logger = logger;
            this.userServices = userServices;
        }

        [HttpGet]
        [Route("getUser")]
        //[FeatureGate("IsActive")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var access = Request.Headers["Authorization"];
                var result = await userServices.GetUserInformation();
                return Ok(result);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
