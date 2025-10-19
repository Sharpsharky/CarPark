using CarPark.Domain.Enums;
using CarPark.Domain.Errors;

namespace CarPark.Domain.Entities
{
    public class ParkingTicket
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string VehicleReg { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public int SpaceNumber { get; private set; }
        public DateTime TimeInUtc { get; private set; }
        public DateTime? TimeOutUtc { get; private set; }
        public decimal? Charge { get; private set; }

        public ParkingTicket(string vehicleReg, VehicleType vehicleType, int spaceNumber, DateTime timeInUtc)
        {
            if (string.IsNullOrWhiteSpace(vehicleReg)) throw new DomainException("VehicleReg is required.");
            if (spaceNumber <= 0) throw new DomainException("SpaceNumber must be positive.");
            if (timeInUtc.Kind != DateTimeKind.Utc) throw new DomainException("TimeIn must be UTC.");

            VehicleReg = vehicleReg.Trim().ToUpperInvariant();
            VehicleType = vehicleType;
            SpaceNumber = spaceNumber;
            TimeInUtc = timeInUtc;
        }

        public void Close(DateTime timeOutUtc, decimal charge)
        {
            if (timeOutUtc.Kind != DateTimeKind.Utc) throw new DomainException("TimeOut must be UTC.");
            if (timeOutUtc < TimeInUtc) throw new DomainException("TimeOut must be after TimeIn.");
            if (Charge is not null || TimeOutUtc is not null) throw new DomainException("Ticket already closed.");
            if (charge < 0) throw new DomainException("Charge must be non-negative.");

            TimeOutUtc = timeOutUtc;
            Charge = decimal.Round(charge, 2, MidpointRounding.AwayFromZero);
        }
    }
}
