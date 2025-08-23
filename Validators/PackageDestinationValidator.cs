using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class PackageDestinationValidator : AbstractValidator<PackageDestination>
    {
        public PackageDestinationValidator()
        {

            RuleFor(x => x.PackageId)
                .NotNull().WithMessage("Package is required.");


            RuleFor(x => x.DestinationId)
                .NotNull().WithMessage("Destination is required.");


            RuleFor(x => x.OrderInTour)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Order In Tour is required.")
                .NotEmpty().WithMessage("Order In Tour cannot be empty.");

        }
    }
}
