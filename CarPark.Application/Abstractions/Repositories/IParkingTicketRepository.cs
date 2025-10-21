using CarPark.Domain.Entities;


namespace CarPark.Application.Abstractions.Repositories
{
    public interface IParkingTicketRepository
    {
        Task AddAsync(ParkingTicket ticket, CancellationToken ct);
        Task<ParkingTicket?> GetActiveByVehicleRegAsync(string vehicleReg, CancellationToken ct);
        Task UpdateAsync(ParkingTicket ticket, CancellationToken ct);
    }
}
