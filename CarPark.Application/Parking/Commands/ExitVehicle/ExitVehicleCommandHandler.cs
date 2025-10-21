using CarPark.Application.Abstractions.Repositories;
using CarPark.Application.Abstractions.Time;
using CarPark.Application.Common.Exceptions;
using CarPark.Domain.Policies;
using MediatR;

namespace CarPark.Application.Parking.Commands.ExitVehicle
{
    public class ExitVehicleCommandHandler : IRequestHandler<ExitVehicleCommand, ExitVehicleResult>
    {
        private readonly IParkingTicketRepository _tickets;
        private readonly IParkingSpaceRepository _spaces;
        private readonly IPricingPolicy _pricing;
        private readonly IClock _clock;

        public ExitVehicleCommandHandler(
            IParkingTicketRepository tickets,
            IParkingSpaceRepository spaces,
            IPricingPolicy pricing,
            IClock clock)
        {
            _tickets = tickets;
            _spaces = spaces;
            _pricing = pricing;
            _clock = clock;
        }

        public async Task<ExitVehicleResult> Handle(ExitVehicleCommand cmd, CancellationToken ct)
        {
            var normalizedReg = cmd.VehicleReg.Trim().ToUpperInvariant();
            var ticket = await _tickets.GetActiveByVehicleRegAsync(normalizedReg, ct);

            if (ticket is null)
                throw new NotFoundException($"No active ticket for vehicle '{normalizedReg}'.");

            var now = _clock.UtcNow;
            var charge = _pricing.CalculateCharge(ticket.VehicleType, ticket.TimeInUtc, now);

            ticket.Close(now, charge);
            await _tickets.UpdateAsync(ticket, ct);

            var space = await _spaces.GetByNumberAsync(ticket.SpaceNumber, ct);
            if (space is not null)
            {
                space.Release();
                await _spaces.UpdateAsync(space, ct);
            }

            return new ExitVehicleResult(normalizedReg, charge, ticket.TimeInUtc, now);
        }
    }
}
