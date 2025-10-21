using CarPark.Application.Parking.Commands.AllocateVehicle;
using FluentValidation;
namespace CarPark.Application.Parking.Validators
{
    public class AllocateVehicleCommandValidator : AbstractValidator<AllocateVehicleCommand>
    {
        public AllocateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleReg)
                .NotEmpty().WithMessage("VehicleReg is required.")
                .MaximumLength(32);
            RuleFor(x => x.VehicleType)
                .IsInEnum().WithMessage("VehicleType must be Small(1), Medium(2), Large(3).");
        }
    }
}
