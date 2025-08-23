using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class PackageValidator : AbstractValidator<MstPackage>
    {
        public PackageValidator()
        {
            RuleFor(x => x.PackageCode)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("PackageCode is required.")
                .NotEmpty().WithMessage("BirthDate cannot be empty.")
                .Matches("^PK\\d{3}$").WithMessage("Package Code must be in the formate like PK001.");

            RuleFor(x => x.PackageName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("PackageName is required.")
                .NotEmpty().WithMessage("PackageName cannot be empty.")
                .Matches("^[A-Za-z ]*$").WithMessage("PackageName does not contains any special character or numeric character.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Description is required.")
                .NotEmpty().WithMessage("Discription cannot be empty.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Price is required.")
                .NotEmpty().WithMessage("Price cannot be empty.");

            RuleFor(x => x.DurationDays)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("DurationDays is required.")
                .NotEmpty().WithMessage("DurationDays cannot be empty.");

            RuleFor(x => x.DurationNights)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("DurationNights is required.")
                .NotEmpty().WithMessage("DurationNights cannot be empty.");

            RuleFor(x => x.AvailabilityStatus)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Availability Status is required.")
                .NotEmpty().WithMessage("Availability Status cannot be empty.");

            RuleFor(x => x.Category)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Category is required.")
                .NotEmpty().WithMessage("Category cannot be empty.");

            RuleFor(x => x.ImageUrl)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Image URL is required.")
                .NotEmpty().WithMessage("Image URL cannot be empty.");

            RuleFor(x => x.CancellationPolicy)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Cancellation Policy is required.")
                .NotEmpty().WithMessage("Cancellation Policy cannot be empty.");


            RuleFor(x => x.UserId)
              .NotNull().WithMessage("User is Required");
        }
    }
}
