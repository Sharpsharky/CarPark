using CarPark.Domain.Enums;
using CarPark.Domain.Errors;
using CarPark.Domain.ValueObjects;

namespace CarPark.Domain.Entities
{
    public class ParkingSpace
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int Number { get; private set; } // (1..N)
        public bool IsOccupied { get; private set; }
        public string? VehicleReg { get; private set; }
        public VehicleType? VehicleType { get; private set; }
        public DateTime? TimeInUtc { get; private set; }

        public ParkingSpace(int number)
        {
            if (number <= 0) throw new DomainException("Space number must be positive.");
            Number = number;
        }

        public void Occupy(string vehicleReg, VehicleType vehicleType, DateTime timeInUtc)
        {
            if (IsOccupied) throw new DomainException($"Space {Number} is already occupied.");
            if (string.IsNullOrWhiteSpace(vehicleReg)) throw new DomainException("VehicleReg is required.");
            if (timeInUtc.Kind != DateTimeKind.Utc) throw new DomainException("TimeIn must be in UTC.");

            IsOccupied = true;
            VehicleReg = vehicleReg.Trim().ToUpperInvariant();
            VehicleType = vehicleType;
            TimeInUtc = timeInUtc;
        }

        public ReleasedInfo Release()
        {
            if (!IsOccupied || VehicleReg is null || VehicleType is null || TimeInUtc is null)
                throw new DomainException("Space is not occupied.");

            var info = new ReleasedInfo(VehicleReg, VehicleType.Value, TimeInUtc.Value);

            IsOccupied = false;
            VehicleReg = null;
            VehicleType = null;
            TimeInUtc = null;

            return info;
        }
    }
}
