using CarPark.Domain.Entities;
using CarPark.Domain.Enums;
using CarPark.Domain.Errors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarPark.Tests.Domain
{
    public class ParkingSpaceTests
    {
        [Fact]
        public void Occupy_EmptySpace_ShouldSetState()
        {
            var space = new ParkingSpace(1);
            var t = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);

            space.Occupy("WX12345", VehicleType.Medium, t);

            space.IsOccupied.Should().BeTrue();
            space.VehicleReg.Should().Be("WX12345");
            space.VehicleType.Should().Be(VehicleType.Medium);
            space.TimeInUtc.Should().Be(t);
        }

        [Fact]
        public void Occupy_AlreadyOccupied_ShouldThrow()
        {
            var space = new ParkingSpace(1);
            var t = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            space.Occupy("WX12345", VehicleType.Medium, t);

            Action act = () => space.Occupy("AB1", VehicleType.Small, t.AddMinutes(1));

            act.Should().Throw<DomainException>()
               .WithMessage("*already occupied*");
        }

        [Fact]
        public void Release_WhenOccupied_ShouldClearAndReturnInfo()
        {
            var space = new ParkingSpace(5);
            var t = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            space.Occupy("WX12345", VehicleType.Large, t);

            var info = space.Release();

            info.VehicleReg.Should().Be("WX12345");
            info.VehicleType.Should().Be(VehicleType.Large);
            info.TimeInUtc.Should().Be(t);

            space.IsOccupied.Should().BeFalse();
            space.VehicleReg.Should().BeNull();
            space.VehicleType.Should().BeNull();
            space.TimeInUtc.Should().BeNull();
        }

        [Fact]
        public void Release_WhenNotOccupied_ShouldThrow()
        {
            var space = new ParkingSpace(2);
            Action act = () => space.Release();
            act.Should().Throw<DomainException>()
               .WithMessage("*not occupied*");
        }
    }
}
