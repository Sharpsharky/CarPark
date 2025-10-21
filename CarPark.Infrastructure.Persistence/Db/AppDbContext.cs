using CarPark.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Infrastructure.Persistence.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ParkingSpace> ParkingSpaces => Set<ParkingSpace>();
        public DbSet<ParkingTicket> ParkingTickets => Set<ParkingTicket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
