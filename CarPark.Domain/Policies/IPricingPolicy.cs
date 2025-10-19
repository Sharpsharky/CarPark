using CarPark.Domain.Enums;

namespace CarPark.Domain.Policies
{
    public interface IPricingPolicy
    {
        decimal CalculateCharge(VehicleType vehicleType, DateTime timeInUtc, DateTime timeOutUtc);
    }
}
