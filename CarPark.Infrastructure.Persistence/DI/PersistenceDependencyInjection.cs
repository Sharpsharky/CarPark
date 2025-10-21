using CarPark.Application.Abstractions.Repositories;
using CarPark.Application.Abstractions.Time;
using CarPark.Infrastructure.Persistence.Db;
using CarPark.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.Infrastructure.Persistence.DI
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("Default")
                     ?? throw new InvalidOperationException("Missing ConnectionStrings:Default.");

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(cs, npgsql =>
                {
                    npgsql.MigrationsHistoryTable("__ef_migrations_history", "public");
                }));

            services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
            services.AddScoped<IParkingTicketRepository, ParkingTicketRepository>();

            return services;
        }
    }
}
