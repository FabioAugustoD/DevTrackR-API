using DevTrackR.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevTrackR.API.Persistence
{
    public class DevTrackRContext : DbContext
    {
        public DevTrackRContext(DbContextOptions<DevTrackRContext> options) : base(options)
        {
            
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageUpdate> PackageUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Package>(entity =>
            {
                // e.ToTable("tb_package");
                entity
                .HasKey(e => e.Id);

                entity
                .HasMany(e => e.Updates)
                .WithOne().HasForeignKey(e => e.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PackageUpdate>(e =>
            {
                //e.ToTable("tb_packageUpdate");
                e.HasKey(e => e.Id);
            });
        }

    }
}
