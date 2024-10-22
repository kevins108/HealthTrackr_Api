using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [Required]
    [MaxLength(100)]
    public string? EmailAddress { get; set; }
    public DateTime? CreateDate { get; set; }
    public bool Active { get; set; }
    [Required]
    [MaxLength(100)]
    public string? UserName { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Password { get; set; }
}
