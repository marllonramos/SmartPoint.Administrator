using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartPoint.Administrator.Infra.Identity.Context
{
    public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();

            optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=postgres;Timeout=30;SearchPath=smartpointidentity");

            return new ApplicationIdentityDbContext(optionsBuilder.Options);
        }
    }
}
