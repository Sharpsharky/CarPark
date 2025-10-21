using CarPark.Application.Abstractions.Repositories;
using CarPark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CarPark.Infrastructure.Persistence.Db;

namespace CarPark.Infrastructure.Persistence.Repositories
{
    public class ParkingTicketRepository : IParkingTicketRepository
    {
        private readonly AppDbContext _db;
        public ParkingTicketRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(ParkingTicket ticket, CancellationToken ct)
        {
            await _db.ParkingTickets.AddAsync(ticket, ct);
            await _db.SaveChangesAsync(ct);
        }

        public Task<ParkingTicket?> GetActiveByVehicleRegAsync(string vehicleReg, CancellationToken ct)
            => _db.ParkingTickets
                .AsTracking()
                .Where(t => t.VehicleReg == vehicleReg && t.TimeOutUtc == null)
                .OrderByDescending(t => t.TimeInUtc)
                .FirstOrDefaultAsync(ct);

        public async Task UpdateAsync(ParkingTicket ticket, CancellationToken ct)
        {
            _db.ParkingTickets.Update(ticket);
            await _db.SaveChangesAsync(ct);
        }
    }
}
