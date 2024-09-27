using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public Guid UserGuid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool Active { get; set; }
}
