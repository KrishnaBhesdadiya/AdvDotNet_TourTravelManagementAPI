using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class TravelerValidator : AbstractValidator<MstTraveler>
    {
        public TravelerValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("FirstName is Required")
                .NotEmpty().WithMessage("FirstName No is Not Null")
                .Matches("^[A-Za-z ]*$").WithMessage("Only Character is valid");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("LastName is Required")
                .NotEmpty().WithMessage("LastName No is Not Null")
                .Matches("^[A-Za-z ]*$").WithMessage("Only Character is valid");

            RuleFor(x => x.DateOfBirth)
               .NotNull().WithMessage("DateOfBirth is Required");

            RuleFor(x => x.PassportNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("PassportNumber is Required")
                .NotEmpty().WithMessage("PassportNumber No is Not Null");

            RuleFor(x => x.PassportExpiryDate)
               .NotNull().WithMessage("PassportExpiryDate is Required");

            RuleFor(x => x.EmergencyContactName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("EmergencyContactName is Required")
               .Matches("^[A-Za-z ]*$").WithMessage("Only Character is valid");

            RuleFor(x => x.EmergencyContactNumber)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("EmergencyContactName is Required")
               .Length(10).WithMessage("ContactName is not Valid");


            RuleFor(x => x.UserId)
               .NotNull().WithMessage("User is Required");


        }
    }
}
