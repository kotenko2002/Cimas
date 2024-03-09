using Cimas.Domain.Entities.Halls;
using FluentValidation;
using FluentValidation.Results;

namespace Cimas.Application.Features.Halls.Commands.UpdateHallSeats
{
    public class UpdateHallSeatsCommandValidator : AbstractValidator<UpdateHallSeatsCommand>
    {
        public UpdateHallSeatsCommandValidator()
        {
            RuleFor(x => x.HallId)
               .NotEmpty();

            RuleFor(x => x.Seats)
                .NotEmpty()
                .Must(HaveUniqueIds)
                .WithMessage("All seat Ids must be unique")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Seats)
                    .Custom((seats, context) => AreValidSeats(seats, context));
                });
        }

        private bool HaveUniqueIds(List<UpdateSeat> seats)
            => seats.DistinctBy(seat => seat.Id).Count() == seats.Count;

        private bool AreValidSeats(List<UpdateSeat> seats, ValidationContext<UpdateHallSeatsCommand> context)
        {
            var invalidSeats = seats.Where(seat => !IsValiSeat(seat)).ToList();

            foreach (var invalidSeat in invalidSeats)
            {
                context.AddFailure(new ValidationFailure(
                    "Seats",
                    $"Seat with Id: {invalidSeat.Id} is not valid. Please ensure the seat has a valid Id, and has a valid status"));
            }

            return !invalidSeats.Any();
        }
        private bool IsValiSeat(UpdateSeat seat)
            => seat.Id != Guid.Empty && Enum.IsDefined(typeof(HallSeatStatus), seat.Status);
    }
}
