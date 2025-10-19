using CarPark.Domain.Enums;
using FluentValidation;

namespace CarPark.Application.Parking.Commands.AllocateVehicle
{
    public class AllocateVehicleCommandValidator : AbstractValidator<AllocateVehicleCommand>
    {
        public AllocateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleReg).NotEmpty().MaximumLength(32);
            RuleFor(x => x.VehicleType)
                .Must(v => v is VehicleType.Small or VehicleType.Medium or VehicleType.Large)
                .WithMessage("VehicleType must be 1, 2 or 3.");
        }
    }
}
