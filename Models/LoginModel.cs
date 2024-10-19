using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models;

public class LoginModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class AccountModel
{
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(100)]
    public string? UserName { get; set; }
    [MaxLength(100)]
    public string? Password { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
}
