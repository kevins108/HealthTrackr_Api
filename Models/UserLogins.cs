using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
    public class UserLogins
    {
        [Key]
        public long UserLoginId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime LoginDate { get; set; }
    }
}
