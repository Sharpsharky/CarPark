using MediatR;

namespace CarPark.Application.Parking.Commands.ExitVehicle
{
    public record ExitVehicleCommand(string VehicleReg) : IRequest<ExitVehicleResult>;

    public record ExitVehicleResult(string VehicleReg, decimal VehicleCharge, DateTime TimeInUtc, DateTime TimeOutUtc);
}
