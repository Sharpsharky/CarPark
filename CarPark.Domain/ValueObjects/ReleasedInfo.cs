using CarPark.Domain.Enums;

namespace CarPark.Domain.ValueObjects
{
    public sealed record ReleasedInfo(string VehicleReg, VehicleType VehicleType, DateTime TimeInUtc);
}
