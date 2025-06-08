using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Infra.Administrator.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Point> Points { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("smartpoint");
        }
    }
}
