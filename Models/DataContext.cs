using HealthTrackr_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackr_Api.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<ActivityRecords> ActivityRecords { get; set; }
        public virtual DbSet<ActivityTypes> ActivityTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
