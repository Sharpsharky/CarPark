using CarPark.Domain.Config;
using CarPark.Domain.Enums;
using CarPark.Domain.Policies;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace CarPark.Tests.Domain
{
    public class PricingPolicyTests
    {
        private static DefaultPricingPolicy Policy(
            decimal small = 0.10m,
            decimal medium = 0.20m,
            decimal large = 0.40m,
            decimal extra = 1.00m)
        {
            var options = Options.Create(new PricingOptions
            {
                SmallRatePerMinute = small,
                MediumRatePerMinute = medium,
                LargeRatePerMinute = large,
                ExtraPerFiveMinutes = extra
            });

            return new DefaultPricingPolicy(options);
        }

        [Fact]
        public void ZeroMinutes_ShouldCostZero()
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            var sut = Policy();

            var cost = sut.CalculateCharge(VehicleType.Small, now, now);

            cost.Should().Be(0m);
        }

        [Theory]
        [InlineData(1, 0.10)]  // 1 min * 0.10 + 0 extra
        [InlineData(4, 0.40)]  // 4 * 0.10 + 0
        [InlineData(5, 1.50)]  // 5 * 0.10 + 1 extra
        [InlineData(6, 1.60)]  // 6 * 0.10 + 1
        [InlineData(10, 3.00)] // 10 * 0.10 + 2
        public void SmallCar_WithExtraPer5Min(int minutes, decimal expected)
        {
            var start = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var end = start.AddMinutes(minutes);
            var sut = Policy();

            var cost = sut.CalculateCharge(VehicleType.Small, start, end);

            cost.Should().Be(expected);
        }

        [Fact]
        public void NonUtcTimes_ShouldThrow()
        {
            var sut = Policy();
            var start = DateTime.Now;
            var end = start.AddMinutes(5);

            Action act = () => sut.CalculateCharge(VehicleType.Small, start, end);
            act.Should().Throw<Exception>().WithMessage("*UTC*");
        }

        [Fact]
        public void TimeOutBeforeTimeIn_ShouldThrow()
        {
            var sut = Policy();
            var end = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var start = end.AddMinutes(1);

            Action act = () => sut.CalculateCharge(VehicleType.Small, start, end);
            act.Should().Throw<Exception>().WithMessage("*after*");
        }
    }

}
