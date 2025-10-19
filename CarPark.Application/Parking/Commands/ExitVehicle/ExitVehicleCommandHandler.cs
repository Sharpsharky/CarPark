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

        public async Task<ExitVehicleResult> Handle(ExitVehicleCommand request, CancellationToken ct)
        {
            var ticket = await _tickets.GetActiveByVehicleRegAsync(request.VehicleReg.Trim().ToUpperInvariant(), ct);
            if (ticket is null)
                throw new NotFoundException($"Active ticket for vehicle '{request.VehicleReg}' not found.");

            var space = await _spaces.GetByNumberAsync(ticket.SpaceNumber, ct);
            if (space is null)
                throw new NotFoundException($"Space #{ticket.SpaceNumber} not found.");

            var timeOut = _clock.UtcNow;
            var charge = _pricing.CalculateCharge(ticket.VehicleType, ticket.TimeInUtc, timeOut);

            ticket.Close(timeOut, charge);
            space.Release();

            await _tickets.UpdateAsync(ticket, ct);
            await _spaces.UpdateAsync(space, ct);

            return new ExitVehicleResult(ticket.VehicleReg, ticket.Charge!.Value, ticket.TimeInUtc, ticket.TimeOutUtc!.Value);
        }
    }
}
