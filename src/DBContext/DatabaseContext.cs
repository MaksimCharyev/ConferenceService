using Microsoft.EntityFrameworkCore;
using ConferenceService.DBContext.Models;
namespace ConferenceService.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Application> applications { get; set; } = null!;
        public DbSet<User> users { get; set; } = null!;
        public DbSet<SubmittedApplication> submittedApplications { get; set; } = null!;
        public DbSet<Activity> activities { get; set; } = null!;
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, activity = EnumTypeActivity.Report, description = "Доклад, 35 - 45 минут" },
                new Activity { Id = 2, activity = EnumTypeActivity.Discussion, description = "Дискуссия / круглый стол, 40-50 минут" },
                new Activity { Id = 3, activity = EnumTypeActivity.MasterClass, description = "Мастеркласс, 1-2 часа" }
            );

        }
    }
}
