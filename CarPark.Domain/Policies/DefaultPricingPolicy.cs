using CarPark.Domain.Config;
using CarPark.Domain.Enums;
using CarPark.Domain.Errors;
using Microsoft.Extensions.Options;

namespace CarPark.Domain.Policies
{
    public class DefaultPricingPolicy : IPricingPolicy
    {
        private readonly PricingOptions _options;

        public DefaultPricingPolicy(IOptions<PricingOptions> options)
            => _options = options.Value;

        public decimal CalculateCharge(VehicleType vehicleType, DateTime timeInUtc, DateTime timeOutUtc)
        {
            if (timeInUtc.Kind != DateTimeKind.Utc || timeOutUtc.Kind != DateTimeKind.Utc)
                throw new DomainException("Pricing requires UTC times.");
            if (timeOutUtc < timeInUtc)
                throw new DomainException("TimeOut must be after TimeIn.");

            var totalMinutes = (int)Math.Floor((timeOutUtc - timeInUtc).TotalMinutes);
            if (totalMinutes <= 0) return 0m;

            var perMinuteRate = vehicleType switch
            {
                VehicleType.Small => _options.SmallRatePerMinute,
                VehicleType.Medium => _options.MediumRatePerMinute,
                VehicleType.Large => _options.LargeRatePerMinute,
                _ => throw new DomainException("Unknown vehicle type.")
            };

            var baseCost = perMinuteRate * totalMinutes;
            var fiveMinBlocks = totalMinutes / 5;
            var extra = _options.ExtraPerFiveMinutes * fiveMinBlocks;

            return decimal.Round(baseCost + extra, 2, MidpointRounding.AwayFromZero);
        }
    }
}
