using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {

            RuleFor(x => x.BookingId)
                .NotNull().WithMessage("BookingCode is required.");

            RuleFor(x => x.PaymentDate)
                .NotNull().WithMessage("PaymentDate is required.");

            RuleFor(x => x.Amount)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Amount is required.")
                .NotEmpty().WithMessage("Amount cannot be empty.");

            RuleFor(x => x.PaymentMethod)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Payment Method is required.")
                .NotEmpty().WithMessage("Payment Method cannot be empty.");

            RuleFor(x => x.TransactionId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("TransactionID is required.")
                .NotEmpty().WithMessage("TransactionID cannot be empty.");

            RuleFor(x => x.UserId)
              .NotNull().WithMessage("User is Required");
        }
    }
}
