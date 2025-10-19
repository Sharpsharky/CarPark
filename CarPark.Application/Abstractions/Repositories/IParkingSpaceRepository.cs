using CarPark.Domain.Entities;

namespace CarPark.Application.Abstractions.Repositories
{
    public interface IParkingSpaceRepository
    {
        Task<ParkingSpace?> GetFirstAvailableAsync(CancellationToken ct);
        Task<ParkingSpace?> GetByNumberAsync(int spaceNumber, CancellationToken ct);
        Task<int> CountAvailableAsync(CancellationToken ct);
        Task<int> CountOccupiedAsync(CancellationToken ct);
        Task UpdateAsync(ParkingSpace space, CancellationToken ct);
    }
}
