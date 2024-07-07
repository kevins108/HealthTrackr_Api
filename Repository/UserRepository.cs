using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace HealthTrackr_Api.Repository
{
    public class UserRepository
	{
		private readonly DataContext context;
		public ApplicationSettings settings { get; }
		private readonly IFeatureManager featureManager;

		public UserRepository(DataContext context, IOptionsSnapshot<ApplicationSettings> settings, IFeatureManager featureManager)
		{
			this.context = context;
			this.settings = settings.Value;
			this.featureManager = featureManager;
		}

		public async Task<Users?> GetUserInformation()
		{
			var key = settings.KEY;
			var actite = await featureManager.IsEnabledAsync("IsActive");
			var user = new Users();
			try
			{
				user = await context.Users.Where(u => u.UserId > 0).FirstOrDefaultAsync();
			}
			catch (Exception)
			{
				throw;
			}

			return user;
		}
	}
}
