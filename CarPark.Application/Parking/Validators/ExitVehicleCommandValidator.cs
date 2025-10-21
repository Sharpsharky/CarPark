using CarPark.Application.Parking.Commands.ExitVehicle;
using FluentValidation;

namespace CarPark.Application.Parking.Validators
{
    public class ExitVehicleCommandValidator : AbstractValidator<ExitVehicleCommand>
    {
        public ExitVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleReg)
                .NotEmpty().WithMessage("VehicleReg is required.")
                .MaximumLength(32);
        }
    }
}
