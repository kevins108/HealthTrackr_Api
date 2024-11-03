using System.ComponentModel.DataAnnotations;
namespace HealthTrackr_Api.Models
{
    public class ActivityTypes
    {
        [Key]
        public int ActivityTypeId { get; set; }
        [MaxLength(50)]
        public string? ActivityName { get; set; }
    }
}
