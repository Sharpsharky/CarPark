using CarPark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPark.Infrastructure.Persistence.Configurations
{
    public class ParkingTicketConfiguration : IEntityTypeConfiguration<ParkingTicket>
    {
        public void Configure(EntityTypeBuilder<ParkingTicket> b)
        {
            b.ToTable("parking_tickets");
            b.HasKey(x => x.Id);

            b.Property(x => x.VehicleReg)
                .IsRequired()
                .HasMaxLength(32);

            b.Property(x => x.VehicleType)
                .HasConversion<int>();

            b.Property(x => x.SpaceNumber)
                .IsRequired();

            b.Property(x => x.TimeInUtc)
                .IsRequired();

            b.Property(x => x.TimeOutUtc);

            b.Property(x => x.Charge)
                .HasPrecision(18, 2);

            b.HasIndex(x => new { x.VehicleReg, x.TimeOutUtc })
                .HasDatabaseName("ix_ticket_vehicle_active");
        }
    }
}
