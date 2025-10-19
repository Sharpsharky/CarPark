using CarPark.Domain.Config;
using CarPark.Domain.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPark.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PricingOptions>(
                configuration.GetSection(PricingOptions.SectionName));

            services.AddScoped<IPricingPolicy, DefaultPricingPolicy>();

            return services;
        }
    }
}
