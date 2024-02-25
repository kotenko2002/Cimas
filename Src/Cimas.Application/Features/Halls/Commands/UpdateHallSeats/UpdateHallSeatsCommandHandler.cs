using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Halls.Commands.UpdateHallSeats
{
    public class UpdateHallSeatsCommandHandler : IRequestHandler<UpdateHallSeatsCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public UpdateHallSeatsCommandHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateHallSeatsCommand command, CancellationToken cancellationToken)
        {
            Hall hall = await _uow.HallRepository.GetHallWithSeatsByIdAsync(command.HallId);
            if (hall is null)
            {
                return Error.NotFound(description: "Hall with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != await _uow.HallRepository.GetCompanyIdByHallIdAsync(command.HallId))
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            // TODO: implement updating seats status

            return Result.Success;
        }
    }
}
