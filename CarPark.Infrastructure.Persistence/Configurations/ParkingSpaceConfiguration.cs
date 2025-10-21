using CarPark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPark.Infrastructure.Persistence.Configurations
{
    public class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> b)
        {
            b.ToTable("parking_spaces");
            b.HasKey(x => x.Id);

            b.HasIndex(x => x.Number).IsUnique();
            b.Property(x => x.Number).IsRequired();
            b.Property(x => x.IsOccupied).IsRequired();
            b.Property(x => x.VehicleReg).HasMaxLength(32);
            b.Property(x => x.VehicleType).HasConversion<int?>();
            b.Property(x => x.TimeInUtc);

            var seedRows = Enumerable.Range(1, 100).Select(n => new
            {
                Id = Guid.Parse($"00000000-0000-0000-0000-{n.ToString("D12")}"),
                Number = n,
                IsOccupied = false,
                VehicleReg = (string?)null,
                VehicleType = (int?)null,
                TimeInUtc = (DateTime?)null
            });

            b.HasData(seedRows);
        }
    }
}
