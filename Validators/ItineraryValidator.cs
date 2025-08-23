using FluentValidation;
using TourTravel.Models;

namespace TourTravel.Validators
{
    public class ItineraryValidator : AbstractValidator<Itinerary>
    {
        public ItineraryValidator()
        {
            RuleFor(x => x.DayNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("DayNumber is required.")
                .NotEmpty().WithMessage("DayNumber cannot be empty.");

            RuleFor(x => x.ActivityName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("ActivityName is required.")
                .NotEmpty().WithMessage("ActivityName cannot be empty.");

            RuleFor(x => x.ActivityDescription)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Activity Description is required.")
                .NotEmpty().WithMessage("Activity Description cannot be empty.");

            RuleFor(x => x.LocationDetails)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Location Details is required.")
                .NotEmpty().WithMessage("Location Details cannot be empty.");

            RuleFor(x => x.StartTime)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Start Time is required.")
                .NotEmpty().WithMessage("Start Time cannot be empty.");

            RuleFor(x => x.EndTime)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("End Time is required.")
                .NotEmpty().WithMessage("End Time cannot be empty.");

        }
    }
}
