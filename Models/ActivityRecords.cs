using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
    public class ActivityRecords
    {
        [Key]
        public int ActivityRecordId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ActivityTypeId { get; set; }
        public DateTime? ActivityDate { get; set; }
        public float? DurationMinutes { get; set; }
        public float? DistanceMiles { get; set; }
        public bool Active { get; set; }
    }
}