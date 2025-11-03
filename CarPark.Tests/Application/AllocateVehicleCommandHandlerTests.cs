using CarPark.Application.Abstractions.Repositories;
using CarPark.Application.Abstractions.Time;
using CarPark.Application.Common.Exceptions;
using CarPark.Application.Parking.Commands.AllocateVehicle;
using CarPark.Domain.Entities;
using CarPark.Domain.Enums;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarPark.Tests.Application
{
    public class AllocateVehicleCommandHandlerTests
    {
        [Fact]
        public async Task Throws_Conflict_When_NoSpaces()
        {
            var spaces = new Mock<IParkingSpaceRepository>();
            var tickets = new Mock<IParkingTicketRepository>();
            var clock = new Mock<IClock>();
            spaces.Setup(s => s.CountAvailableAsync(default)).ReturnsAsync(0);
            spaces.Setup(s => s.GetFirstAvailableAsync(default)).ReturnsAsync((ParkingSpace?)null);

            var sut = new AllocateVehicleCommandHandler(spaces.Object, tickets.Object, clock.Object);

            var act = async () => await sut.Handle(new AllocateVehicleCommand("WX12345", VehicleType.Small), default);

            await act.Should().ThrowAsync<ConflictException>()
                .WithMessage("*No available spaces*");
        }

        [Fact]
        public async Task Success_When_SpaceAvailable()
        {
            var space = new ParkingSpace(number: 1);
            var spaces = new Mock<IParkingSpaceRepository>();
            var tickets = new Mock<IParkingTicketRepository>();
            var clock = new Mock<IClock>();
            clock.SetupGet(c => c.UtcNow).Returns(new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc));
            spaces.Setup(s => s.CountAvailableAsync(default)).ReturnsAsync(10);
            spaces.Setup(s => s.GetFirstAvailableAsync(default)).ReturnsAsync(space);

            var sut = new AllocateVehicleCommandHandler(spaces.Object, tickets.Object, clock.Object);

            var result = await sut.Handle(new AllocateVehicleCommand("WX12345", VehicleType.Small), default);

            result.SpaceNumber.Should().Be(1);
            spaces.Verify(s => s.UpdateAsync(It.Is<ParkingSpace>(p => p.IsOccupied), default));
            tickets.Verify(t => t.AddAsync(It.Is<ParkingTicket>(p => !p.LowAvailabilitySurchargeApplied), default));
        }

        [Fact]
        public async Task Applies_Surcharge_When_LessThanFiveSpacesRemain()
        {
            var space = new ParkingSpace(number: 1);
            var spaces = new Mock<IParkingSpaceRepository>();
            var tickets = new Mock<IParkingTicketRepository>();
            var clock = new Mock<IClock>();
            clock.SetupGet(c => c.UtcNow).Returns(new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc));
            spaces.Setup(s => s.CountAvailableAsync(default)).ReturnsAsync(4);
            spaces.Setup(s => s.GetFirstAvailableAsync(default)).ReturnsAsync(space);

            var sut = new AllocateVehicleCommandHandler(spaces.Object, tickets.Object, clock.Object);

            await sut.Handle(new AllocateVehicleCommand("WX12345", VehicleType.Small), default);

            tickets.Verify(t => t.AddAsync(It.Is<ParkingTicket>(p => p.LowAvailabilitySurchargeApplied), default));
        }
    }
}
