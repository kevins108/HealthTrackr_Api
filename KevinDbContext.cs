using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackr_Api;

public partial class KevinDbContext : DbContext
{
    public KevinDbContext()
    {
    }

    public KevinDbContext(DbContextOptions<KevinDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:s29efm114alyqlwt.database.windows.net,1433;Initial Catalog=KEVIN_DB;Persist Security Info=False;User ID=kevins108;Password=uW8H1Y6wdpK857paT65DbxyKx7C;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
