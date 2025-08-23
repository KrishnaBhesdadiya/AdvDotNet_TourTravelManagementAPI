using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class DestinationValidator : AbstractValidator<MstDestination>
    {
        public DestinationValidator()
        {
            RuleFor(x => x.DestinationCode)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("DestinationCode is Required")
                .NotEmpty().WithMessage("DestinationCode is Not Null");

            RuleFor(x => x.DestinationName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("DestinationName is Required")
                .NotEmpty().WithMessage("DestinationName No is Not Null")
                .Matches("^[A-Za-z ]*$").WithMessage("Only Character is valid");

            RuleFor(x => x.Country)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Country is Required")
                .NotEmpty().WithMessage("Country No is Not Null")
                .Matches("^[A-Za-z ]*$").WithMessage("Only Character is valid");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Description is Required")
                .NotEmpty().WithMessage("Description No is Not Null");


        }
    }
}
