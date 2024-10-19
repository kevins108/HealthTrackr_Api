using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthTrackr_Api.Repository
{
    public class AccessRepository
    {
        private readonly DataContext context;
        public ApplicationSettings settings { get; }
        private readonly IConfiguration configuration;

        public AccessRepository(DataContext context, IOptionsSnapshot<ApplicationSettings> settings, IConfiguration configuration)
        {
            this.context = context;
            this.settings = settings.Value;
            this.configuration = configuration;
        }

        public async Task<bool> ChangePassword(LoginModel login)
        {
            return true;
        }

        public async Task<User?> ValidateCredentials(LoginModel login)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName);
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
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("Authentication:SecretKey")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));

            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Authentication:Issuer"),
                audience: configuration.GetValue<string>("Authentication:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> CreateUserAccount(AccountModel account)
        {
            try
            {
                var userExists = await context.Users.FirstOrDefaultAsync(x => x.UserName == account.UserName);
                var emailExists = await context.Users.FirstOrDefaultAsync(x => x.EmailAddress == account.Email);

                if (userExists != null)
                {
                    throw new Exception("User already exists");
                }
                else if (emailExists != null)
                {
                    throw new Exception("Email already exists");
                }
                else
                {
                    var user = new User
                    {
                        UserGuid = Guid.NewGuid(),
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        UserName = account.UserName,
                        // TODO: Create password encryption
                        Password = account.Password,
                        EmailAddress = account.Email,
                        CreateDate = DateTime.Now,
                        Active = true
                    };
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
