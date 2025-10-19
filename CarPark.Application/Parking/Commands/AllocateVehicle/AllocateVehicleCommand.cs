using CarPark.Domain.Enums;
using MediatR;

namespace CarPark.Application.Parking.Commands.AllocateVehicle
{

    public record AllocateVehicleCommand(string VehicleReg, VehicleType VehicleType)
        : IRequest<AllocateVehicleResult>;

    public record AllocateVehicleResult(string VehicleReg, int SpaceNumber, DateTime TimeInUtc);
}
