using CarPark.Application.Parking.Commands.AllocateVehicle;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CarPark.Tests.Application
{
    public class AllocateVehicleCommandValidatorTests
    {
        private readonly AllocateVehicleCommandValidator _sut = new();

        [Theory]
        [InlineData("", 1)]
        [InlineData("   ", 2)]
        public void Invalid_WhenMissingReg(string reg, int type)
            => _sut.TestValidate(new AllocateVehicleCommand(reg, (CarPark.Domain.Enums.VehicleType)type))
                   .ShouldHaveValidationErrorFor(x => x.VehicleReg);

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void Invalid_WhenTypeOutOfRange(int badType)
        {
            var result = _sut.TestValidate(new AllocateVehicleCommand("WX12345", (CarPark.Domain.Enums.VehicleType)badType));
            result.Errors.Should().NotBeEmpty();
        }
    }
}
