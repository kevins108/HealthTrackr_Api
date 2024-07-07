using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
	public class UserLogin
	{
		[Key]
		public int LoginId { get; set; }
		public Users? UserGuid { get; set; }
		public DateTime LoginDate { get; set; }
	}
}
