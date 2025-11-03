using System;
using System.Threading.Tasks;
using CarPark.Application.Abstractions.Repositories;
using CarPark.Application.Abstractions.Time;
using CarPark.Application.Common.Exceptions;
using CarPark.Application.Parking.Commands.ExitVehicle;
using CarPark.Domain.Entities;
using CarPark.Domain.Enums;
using CarPark.Domain.Policies;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarPark.Tests.Application
{
    public class ExitVehicleCommandHandlerTests
    {
        [Fact]
        public async Task Throws_NotFound_When_NoTicket()
        {
            var tickets = new Mock<IParkingTicketRepository>();
            var spaces = new Mock<IParkingSpaceRepository>();
            var pricing = new Mock<IPricingPolicy>();
            var clock = new Mock<IClock>();
            tickets.Setup(t => t.GetActiveByVehicleRegAsync("WX12345", default)).ReturnsAsync((ParkingTicket?)null);

            var sut = new ExitVehicleCommandHandler(tickets.Object, spaces.Object, pricing.Object, clock.Object);

            var act = async () => await sut.Handle(new ExitVehicleCommand("wx12345"), default);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Adds_Surcharge_When_Applied()
        {
            var timeIn = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var timeOut = timeIn.AddHours(1);
            var ticket = new ParkingTicket("WX12345", VehicleType.Small, 1, timeIn, lowAvailabilitySurchargeApplied: true);

            var space = new ParkingSpace(1);
            space.Occupy("WX12345", VehicleType.Small, timeIn);

            var tickets = new Mock<IParkingTicketRepository>();
            var spaces = new Mock<IParkingSpaceRepository>();
            var pricing = new Mock<IPricingPolicy>();
            var clock = new Mock<IClock>();

            tickets.Setup(t => t.GetActiveByVehicleRegAsync("WX12345", default)).ReturnsAsync(ticket);
            tickets.Setup(t => t.UpdateAsync(It.IsAny<ParkingTicket>(), default)).Returns(Task.CompletedTask);
            spaces.Setup(s => s.GetByNumberAsync(1, default)).ReturnsAsync(space);
            spaces.Setup(s => s.UpdateAsync(It.IsAny<ParkingSpace>(), default)).Returns(Task.CompletedTask);
            pricing.Setup(p => p.CalculateCharge(VehicleType.Small, timeIn, timeOut)).Returns(10m);
            clock.SetupGet(c => c.UtcNow).Returns(timeOut);

            var sut = new ExitVehicleCommandHandler(tickets.Object, spaces.Object, pricing.Object, clock.Object);

            var result = await sut.Handle(new ExitVehicleCommand("wx12345"), default);

            result.VehicleCharge.Should().Be(15m);
            ticket.Charge.Should().Be(15m);
            ticket.TimeOutUtc.Should().Be(timeOut);
            spaces.Verify(s => s.UpdateAsync(It.Is<ParkingSpace>(p => !p.IsOccupied), default));
        }
    }
}
