using CarPark.Application.Abstractions.Repositories;
using MediatR;

namespace CarPark.Application.Parking.Queries.GetCapacity
{
    public class GetCapacityQueryHandler : IRequestHandler<GetCapacityQuery, GetCapacityResult>
    {
        private readonly IParkingSpaceRepository _spaces;

        public GetCapacityQueryHandler(IParkingSpaceRepository spaces) => _spaces = spaces;

        public async Task<GetCapacityResult> Handle(GetCapacityQuery request, CancellationToken ct)
        {
            var available = await _spaces.CountAvailableAsync(ct);
            var occupied = await _spaces.CountOccupiedAsync(ct);
            return new GetCapacityResult(available, occupied);
        }
    }
}
