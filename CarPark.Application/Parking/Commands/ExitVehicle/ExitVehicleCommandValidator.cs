using FluentValidation;

namespace CarPark.Application.Parking.Commands.ExitVehicle
{
    public class ExitVehicleCommandValidator : AbstractValidator<ExitVehicleCommand>
    {
        public ExitVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleReg).NotEmpty().MaximumLength(32);
        }
    }
}
