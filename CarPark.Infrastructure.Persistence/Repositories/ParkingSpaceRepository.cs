using CarPark.Application.Abstractions.Repositories;
using CarPark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CarPark.Infrastructure.Persistence.Db;

namespace CarPark.Infrastructure.Persistence.Repositories
{
    public class ParkingSpaceRepository : IParkingSpaceRepository
    {
        private readonly AppDbContext _db;
        public ParkingSpaceRepository(AppDbContext db) => _db = db;

        public async Task<ParkingSpace?> GetFirstAvailableAsync(CancellationToken ct)
            => await _db.ParkingSpaces
                .AsTracking()
                .Where(s => !s.IsOccupied)
                .OrderBy(s => s.Number)
                .FirstOrDefaultAsync(ct);

        public async Task<ParkingSpace?> GetByNumberAsync(int spaceNumber, CancellationToken ct)
            => await _db.ParkingSpaces.AsTracking()
                .FirstOrDefaultAsync(s => s.Number == spaceNumber, ct);

        public Task<int> CountAvailableAsync(CancellationToken ct)
            => _db.ParkingSpaces.CountAsync(s => !s.IsOccupied, ct);

        public Task<int> CountOccupiedAsync(CancellationToken ct)
            => _db.ParkingSpaces.CountAsync(s => s.IsOccupied, ct);

        public async Task UpdateAsync(ParkingSpace space, CancellationToken ct)
        {
            _db.ParkingSpaces.Update(space);
            await _db.SaveChangesAsync(ct);
        }
    }
}
