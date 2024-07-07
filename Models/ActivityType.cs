using System.ComponentModel.DataAnnotations;
namespace HealthTrackr_Api.Models
{
	public class ActivityType
	{
		[Key]
		public int ActivityTypeId { get; set; }
		public string? ActivityName { get; set; }
	}
}
