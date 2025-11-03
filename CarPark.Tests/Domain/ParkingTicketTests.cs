using CarPark.Domain.Entities;
using CarPark.Domain.Enums;
using CarPark.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CarPark.Tests.Domain
{
    public class ParkingTicketTests
    {
        [Fact]
        public void Create_Valid_ShouldSetFields()
        {
            var t = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var ticket = new ParkingTicket("DW12345", VehicleType.Small, 7, t);

            ticket.VehicleReg.Should().Be("DW12345");
            ticket.VehicleType.Should().Be(VehicleType.Small);
            ticket.SpaceNumber.Should().Be(7);
            ticket.TimeInUtc.Should().Be(t);
            ticket.TimeOutUtc.Should().BeNull();
            ticket.Charge.Should().BeNull();
            ticket.LowAvailabilitySurchargeApplied.Should().BeFalse();
        }

        [Fact]
        public void Create_WithSurcharge_ShouldSetFlag()
        {
            var t = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var ticket = new ParkingTicket("DW12345", VehicleType.Small, 7, t, lowAvailabilitySurchargeApplied: true);

            ticket.LowAvailabilitySurchargeApplied.Should().BeTrue();
        }

        [Fact]
        public void Close_Valid_ShouldSetChargeAndTimeOut()
        {
            var tIn = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var tOut = tIn.AddMinutes(12);
            var ticket = new ParkingTicket("DW12345", VehicleType.Small, 7, tIn);

            ticket.Close(tOut, 3.40m);

            ticket.TimeOutUtc.Should().Be(tOut);
            ticket.Charge.Should().Be(3.40m);
        }

        [Fact]
        public void Close_Twice_ShouldThrow()
        {
            var tIn = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var tOut = tIn.AddMinutes(5);
            var ticket = new ParkingTicket("DW12345", VehicleType.Small, 7, tIn);
            ticket.Close(tOut, 1.50m);

            Action act = () => ticket.Close(tOut.AddMinutes(1), 1.60m);
            act.Should().Throw<DomainException>()
               .WithMessage("*already closed*");
        }

        [Fact]
        public void Close_WhenTimeOutBeforeTimeIn_ShouldThrow()
        {
            var tIn = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var ticket = new ParkingTicket("DW12345", VehicleType.Small, 7, tIn);

            Action act = () => ticket.Close(tIn.AddMinutes(-1), 0m);
            act.Should().Throw<DomainException>()
               .WithMessage("*after TimeIn*");
        }
    }
}
