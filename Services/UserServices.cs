﻿using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using HealthTrackr_Api.Repository;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace HealthTrackr_Api.Services
{
    public class UserServices
    {
        private ILogger<UserServices> logger;
        private UserRepository userRepository;
        private IFeatureManager featureManager { get; set; }
        public ApplicationSettings settings { get; }


        public UserServices(ILogger<UserServices> logger, UserRepository userRepository, IOptionsSnapshot<ApplicationSettings> settings, IFeatureManager featureManager)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.settings = settings.Value;
            this.featureManager = featureManager;
        }

        public async Task<User> GetUserInformation()
        {
            var key = settings.KEY;
            var actite = await featureManager.IsEnabledAsync(nameof(FeatureFlags.ACTIVE));
            var user = new User();
            try
            {
                var newUser = await userRepository.GetUser();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }


    }
}
