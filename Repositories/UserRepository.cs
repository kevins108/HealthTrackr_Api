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
            try
            {
                return await _context.Users.Where(u => u.UserId > 0).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<object?> GetUserActivities(int userId)
        {
            try
            {
                var activity = await _context.ActivityRecords.Where(ar => ar.UserId == userId)
                .Join(_context.ActivityTypes,
                      ar => ar.ActivityTypeId,
                      at => at.ActivityTypeId,
                      (ar, at) => new
                      {
                          ar.ActivityRecordId,
                          ar.ActivityTypeId,
                          ar.ActivityDate
                      })
                .ToListAsync();

                return activity;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
