using HealthTrackr_Api.Data;
using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackr_Api.Repository
{
    public class UserRepository
    {
        private readonly DataContext dbContext;

        public UserRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User?> GetUser()
        {
            return await dbContext.Users.Where(u => u.UserId > 0).FirstOrDefaultAsync();
        }
    }
}
