using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackr_Api.Data
{
	public partial class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public virtual DbSet<Users> Users { get; set; }
	}
}
