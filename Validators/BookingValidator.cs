using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.BookingDate)
                .NotNull().WithMessage("BookingDate is Required");


            RuleFor(x => x.TravelStartDate)
               .NotNull().WithMessage("TravelStartDate is Required");


            RuleFor(x => x.NumberOfAdults)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("NumberOfAdults is Required")
                .NotEmpty().WithMessage("NumberOfAdults No is Required");

            RuleFor(x => x.NumberOfChildren)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("NumberOfChildren is Required")
                .NotEmpty().WithMessage("NumberOfChildren No is Required");

            RuleFor(x => x.TotalBookingPrice)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("TotalBookingPrice is Required")
                .NotEmpty().WithMessage("TotalBookingPrice No is Required");

            RuleFor(x => x.PaymentStatus)
               .NotNull().WithMessage("PaymentStatus is Required");

            RuleFor(x => x.BookingStatus)
               .NotNull().WithMessage("BookingStatus is Required");

            RuleFor(x => x.UserId)
              .NotNull().WithMessage("User is Required");

            RuleFor(x => x.CustomerId)
              .NotNull().WithMessage("Customer is Required");

            RuleFor(x => x.PackageId)
              .NotNull().WithMessage("Package is Required");


        }
    }
}
