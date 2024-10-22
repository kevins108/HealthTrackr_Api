using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace HealthTrackr_Api.Repository
{
    public class AccessRepository
    {
        private readonly DataContext _context;
        public ApplicationSettings _settings { get; }
        private readonly IConfiguration _configuration;

        public AccessRepository(DataContext context, IOptionsSnapshot<ApplicationSettings> settings, IConfiguration configuration)
        {
            _context = context;
            _settings = settings.Value;
            _configuration = configuration;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel login)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName);
                if (user != null)
                {
                    // compare old password
                    var decryptedPassword = DecryptUserPassword(login.OldPassword);
                    if (ComparePasswords(user.Password, decryptedPassword))
                    {
                        // compare new password
                        if (ComparePasswords(login.NewPassword, login.ConfirmNewPassword))
                        {
                            user.Password = EncryptUserPassword(login.NewPassword);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }



        public async Task<User?> ValidateCredentials(LoginModel login)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName);
                if (user != null)
                {
                    // TODO: Create password encryption
                    if (user.Password == login.Password)
                    {
                        return user;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:SecretKey")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Authentication:Issuer"),
                audience: _configuration.GetValue<string>("Authentication:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CreateUserAccount(AccountModel account)
        {
            try
            {
                var userExists = await _context.Users.AnyAsync(x => x.UserName == account.UserName || x.EmailAddress == account.Email);

                if (userExists)
                {
                    return false;
                }
                else
                {
                    var user = new User
                    {
                        UserGuid = Guid.NewGuid(),
                        FirstName = account?.FirstName?.Trim(),
                        LastName = account?.LastName?.Trim(),
                        UserName = ValidateUserName(account?.UserName),
                        Password = EncryptUserPassword(account?.Password),
                        EmailAddress = ValidateUserEmailAddress(account?.Email),
                        CreateDate = DateTime.Now,
                        Active = true
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ValidateUserEmailAddress(string? email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim();
            }
            else
            {
                throw new ArgumentException("Email address cannot be empty.");
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(email, emailPattern))
            {
                return email;
            }
            else
            {
                throw new ArgumentException("Invalid email address format.");
            }
        }

        private string ValidateUserName(string? userName)
        {
            // already checked if in use, maybe do some cleanup? Trims and lengths?
            return userName;
        }

        private string EncryptUserPassword(string? password)
        {
            if (password != null)
            {
                // Check if password meets the basic requirements
                if (password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit))
                {
                    // TODO: Encrypt password
                    return "";
                }
            }
            return string.Empty;
        }

        private string DecryptUserPassword(string? oldPassword)
        {
            // TODO: Create decrypt password
            return oldPassword ?? string.Empty;
        }

        private bool ComparePasswords(string? password, string? confirmPassword)
        {
            if (!string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(confirmPassword))
            {
                if (password.Equals(confirmPassword, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
