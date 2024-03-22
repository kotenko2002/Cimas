using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Halls.Commands.DeleteHall
{
    public class DeleteHallCommandHandler : IRequestHandler<DeleteHallCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;

        public DeleteHallCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteHallCommand command, CancellationToken cancellationToken)
        {
            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaByIdAsync(command.HallId);
            if (hall is null)
            {
                return Error.NotFound(description: "Hall with such id does not exist");
            }

            User user = await _uow.UserRepository.GetByIdAsync(command.UserId);
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            hall.IsDeleted = true;

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
