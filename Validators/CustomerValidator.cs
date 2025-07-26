using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class CustomerValidator : AbstractValidator<MstUser>
    {
        public CustomerValidator() 
        {
            //Incomplete
            //RuleFor(x => x.UserName).NotNull().WithMessage("Username is required.")
            //    .NotEmpty().WithMessage("UserName cannot be empty.")
            //    .Matches("^[A-Za-z]*$").WithMessage("UserName does not contains any special character or numeric character.");

            //RuleFor(x => x.Password).NotNull().WithMessage("Password is required.").NotEmpty().WithMessage("Password cannot be empty.")
            //    .Length(5, 50).WithMessage("Password must be between 5 to 50 character.")
            //    .Matches("^[A-Za-z]*@(1-9)*$")
            //    .WithMessage("Password can only contains AlphaNumeric character.");

            //RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid Email Format.")
            //    .NotEqual("abc@gmail.com");

            //RuleFor(x => x.MobileNo).NotEmpty().WithMessage("Mobile Number can not be empty.").Length(10).WithMessage("Mobile Number must be 10-digit");
        }
    }
}
