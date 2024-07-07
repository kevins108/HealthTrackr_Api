using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
	public class ActivityRecord
	{
		[Key]
		public int ActivityRecordId { get; set; }
		public Users? UserId { get; set; }
		public ActivityType? ActivityTypeId { get; set; }
		public DateTime ActivityDate { get; set; }
		public int? DurationMinutes { get; set; }
		public int? DistanceMiles { get; set; }
		public bool? Deleted { get; set; }
	}
}
