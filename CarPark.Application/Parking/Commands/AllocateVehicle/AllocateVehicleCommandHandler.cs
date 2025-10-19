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

        public async Task<AllocateVehicleResult> Handle(AllocateVehicleCommand request, CancellationToken ct)
        {
            var space = await _spaces.GetFirstAvailableAsync(ct);
            if (space is null)
                throw new ConflictException("No available spaces.");

            var timeIn = _clock.UtcNow;

            space.Occupy(request.VehicleReg, request.VehicleType, timeIn);

            var ticket = new ParkingTicket(request.VehicleReg, request.VehicleType, space.Number, timeIn);

            await _spaces.UpdateAsync(space, ct);
            await _tickets.AddAsync(ticket, ct);

            return new AllocateVehicleResult(ticket.VehicleReg, ticket.SpaceNumber, ticket.TimeInUtc);
        }
    }
}
