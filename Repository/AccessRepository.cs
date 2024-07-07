using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.Extensions.Options;

namespace HealthTrackr_Api.Repository
{
	public class AccessRepository
	{
		private readonly DataContext context;
		public ApplicationSettings settings { get; }

		public AccessRepository(DataContext context, IOptionsSnapshot<ApplicationSettings> settings)
		{
			this.context = context;
			this.settings = settings.Value;
		}

		public async Task<bool> UserLogin(LoginModel login)
		{
			return true;
		}

		public async Task<bool> ChangePassword(LoginModel login)
		{
			return true;
		}




	}
}
