using Microsoft.Extensions.Options;

namespace HealthTrackr_Api.Data
{
	public class LoginAccess
	{
		public ApplicationSettings settings { get; }

		public LoginAccess(IOptionsSnapshot<ApplicationSettings> settings)
		{
			this.settings = settings.Value;
		}

		// Simply check if the Authorization header matches the key in the appsettings
		// TODO: Will need to expand on this at a later date

		public bool CheckAuthAccess(string auth_header)
		{
			if (string.IsNullOrEmpty(auth_header)) return false;
			var auth_key = settings.AUTHORIZE_KEY;
			if (auth_key == auth_header)
			{
				return true;
			}
			return false;
		}

	}
}
