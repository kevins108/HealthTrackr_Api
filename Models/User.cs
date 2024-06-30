using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
	public class Users
	{
		[Key]
		public int UserId { get; set; }
		public Guid UserGuid { get; set; }
		[MaxLength(50)]
		public string? FirstName { get; set; }
		[MaxLength(50)]
		public string? LastName { get; set; }
		//[MaxLength(50)]
		//public string? EmailAddress { get; set; }
		//public DateTime? CreateDate { get; set; }
		//public bool Active { get; set; }
		//[MaxLength(200)]
		//public string? ProfileUrl { get; set; }
	}
}
