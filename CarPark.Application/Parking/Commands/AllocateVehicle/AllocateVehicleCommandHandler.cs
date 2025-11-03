using CarPark.Application.Abstractions.Repositories;
using CarPark.Application.Abstractions.Time;
using CarPark.Application.Common.Exceptions;
using CarPark.Domain.Entities;
using MediatR;

namespace CarPark.Application.Parking.Commands.AllocateVehicle
{
    public class AllocateVehicleCommandHandler : IRequestHandler<AllocateVehicleCommand, AllocateVehicleResult>
    {
        private readonly IParkingSpaceRepository _spaces;
        private readonly IParkingTicketRepository _tickets;
        private readonly IClock _clock;

        public AllocateVehicleCommandHandler(
            IParkingSpaceRepository spaces,
            IParkingTicketRepository tickets,
            IClock clock)
        {
            _spaces = spaces;
            _tickets = tickets;
            _clock = clock;
        }

        public async Task<AllocateVehicleResult> Handle(AllocateVehicleCommand cmd, CancellationToken ct)
        {
            var normalizedReg = cmd.VehicleReg.Trim().ToUpperInvariant();

            var existingTicket = await _tickets.GetActiveByVehicleRegAsync(normalizedReg, ct);
            if (existingTicket is not null)
                throw new ConflictException($"Vehicle '{normalizedReg}' is already parked in space {existingTicket.SpaceNumber}.");

            var availableSpaces = await _spaces.CountAvailableAsync(ct);
            if (availableSpaces <= 0)
                throw new ConflictException("No available spaces.");

            var space = await _spaces.GetFirstAvailableAsync(ct) ?? throw new ConflictException("No available spaces.");
            var now = _clock.UtcNow;
            space.Occupy(normalizedReg, cmd.VehicleType, now);
            await _spaces.UpdateAsync(space, ct);

            var applySurcharge = availableSpaces < 5;
            var ticket = new ParkingTicket(normalizedReg, cmd.VehicleType, space.Number, now, applySurcharge);
            await _tickets.AddAsync(ticket, ct);

            return new AllocateVehicleResult(normalizedReg, space.Number, now);
        }
    }
}
