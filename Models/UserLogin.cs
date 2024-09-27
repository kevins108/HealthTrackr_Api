using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
    public class UserLogin
    {
        [Key]
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
