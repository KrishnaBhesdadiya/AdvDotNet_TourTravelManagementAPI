using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class CustomerValidator : AbstractValidator<MstCustomer>
    {
        public CustomerValidator() 
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("FirstName is required.")
                .NotEmpty().WithMessage("FirstName cannot be empty.")
                .Matches("^[A-Za-z ]*$").WithMessage("FirstName does not contains any special character or numeric character.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("LastName is required.")
                .NotEmpty().WithMessage("LastName cannot be empty.")
                .Matches("^[A-Za-z ]*$").WithMessage("LastName does not contains any special character or numeric character.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Phone Number is required.")
                .NotEmpty().WithMessage("Phone Number cannot be empty.")
                .Length(10).WithMessage("Phone Number must and only contains ten digits.")
                .Matches("^\\d{10}$").WithMessage("Please Enter valid 10-digit Phone Number.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email Format.")
                .NotEqual("abc@gmail.com");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Address is required.")
                .NotEmpty().WithMessage("Address cannot be empty.");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("BirthDate is required.")
                .NotEmpty().WithMessage("BirthDate cannot be empty.");

            RuleFor(x => x.Nationality)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Nationality is required.")
                .NotEmpty().WithMessage("Nationality cannot be empty.");


            RuleFor(x => x.UserId)
              .NotNull().WithMessage("User is Required");
        }
    }
}
