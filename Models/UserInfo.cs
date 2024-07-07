using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
	public class UserInfo
	{
		[Key]
		public Users? UserGuid { get; set; }
		public string? Password { get; set; }
		public DateTime Saved { get; set; }
	}
}
