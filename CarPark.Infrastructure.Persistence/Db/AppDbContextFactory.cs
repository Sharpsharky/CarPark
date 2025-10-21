using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarPark.Infrastructure.Persistence.Db
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cs = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

            if (string.IsNullOrWhiteSpace(cs))
            {
                Console.WriteLine("[WARN] Using default local DB connection (no environment variable found).");
                cs = "Host=localhost;Port=5432;Database=carparkdb;Username=postgres;Password=postgres";
            }

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(cs, npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", "public"))
                .Options;

            return new AppDbContext(options);
        }
    }
}
