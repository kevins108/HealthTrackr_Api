using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public User? UserGuid { get; set; }
        public string? Password { get; set; }
        public DateTime Saved { get; set; }
    }
}
