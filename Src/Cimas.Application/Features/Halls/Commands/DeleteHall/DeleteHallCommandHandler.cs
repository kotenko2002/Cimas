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
        private readonly ICustomUserManager _userManager;

        public DeleteHallCommandHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteHallCommand command, CancellationToken cancellationToken)
        {
            Hall hall = await _uow.HallRepository.GetByIdAsync(command.HallId);
            if (hall is null)
            {
                return Error.NotFound(description: "Hall with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != await _uow.HallRepository.GetCompanyIdByHallIdAsync(command.HallId))
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            hall.IsDeleted = true;

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
