using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class UserValidator : AbstractValidator<MstUser>
    {
        public UserValidator() 
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("UserName cannot be empty.")
                .Matches("^[A-Za-z ]*$").WithMessage("UserName does not contains any special character or numeric character.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .Length(5,50).WithMessage("Password must be between 5 to 50 character.")
                .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{5,}$")
                .WithMessage("Password can contains AlphaNumeric character with Special Character.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email Format.")
                .NotEqual("abc@gmail.com");

            RuleFor(x => x.MobileNo)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("MobileNo is required.")
                .NotEmpty().WithMessage("MobileNo cannot be empty.")
                .Length(10).WithMessage("MobileNo must and only contains ten digits.")
                .Matches("^\\d{10}$").WithMessage("Please Enter valid 10-digit MobileNo.");

            RuleFor(x => x.Role)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Role is required.")
                .NotEmpty().WithMessage("Role can not be empty.");
        }
    }
}
