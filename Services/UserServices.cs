using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using HealthTrackr_Api.Repository;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace HealthTrackr_Api.Services
{
    public class UserServices
    {
        private ILogger<UserServices> _logger;
        private UserRepository _userRepository;
        private IFeatureManager _featureManager { get; set; }
        public ApplicationSettings _settings { get; }


        public UserServices(ILogger<UserServices> logger, UserRepository userRepository, IOptionsSnapshot<ApplicationSettings> settings, IFeatureManager featureManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _settings = settings.Value;
            _featureManager = featureManager;
        }

        public async Task<User> GetUserInformation()
        {
            //var key = _settings.KEY;
            var actite = await _featureManager.IsEnabledAsync(nameof(FeatureFlags.ACTIVE));
            var user = new User();
            try
            {
                var newUser = await _userRepository.GetUser();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }


    }
}
