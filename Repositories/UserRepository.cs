using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HealthTrackr_Api.Repository
{
    public class UserRepository
    {
        private readonly DataContext _context;
        public ApplicationSettings _settings { get; }
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext context, IOptionsSnapshot<ApplicationSettings> settings, IConfiguration configuration)
        {
            _context = context;
            _settings = settings.Value;
            _configuration = configuration;
        }

        public async Task<User?> GetUser()
        {
            return await _context.Users.Where(u => u.UserId > 0).FirstOrDefaultAsync();
        }
    }
}
