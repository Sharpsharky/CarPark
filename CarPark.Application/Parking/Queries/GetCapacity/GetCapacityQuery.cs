using MediatR;

namespace CarPark.Application.Parking.Queries.GetCapacity
{
    public record GetCapacityQuery() : IRequest<GetCapacityResult>;

    public record GetCapacityResult(int AvailableSpaces, int OccupiedSpaces);
}
