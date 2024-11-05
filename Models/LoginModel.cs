using System.ComponentModel.DataAnnotations;

namespace HealthTrackr_Api.Models;

public class LoginModel
{
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
}

public class AccountModel
{
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(100)]
    public string? Password { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
}

public class ChangePasswordModel
{
    [MaxLength(100)]
    public string? EmailAddress { get; set; }
    [MaxLength(100)]
    public string? OldPassword { get; set; }
    [MaxLength(100)]
    public string? NewPassword { get; set; }
    [MaxLength(100)]
    public string? ConfirmNewPassword { get; set; }
}
