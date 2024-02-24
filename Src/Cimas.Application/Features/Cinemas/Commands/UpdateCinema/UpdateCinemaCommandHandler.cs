using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Commands.UpdateCinema
{
    public class UpdateCinemaCommandHandler : IRequestHandler<UpdateCinemaCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public UpdateCinemaCommandHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateCinemaCommand command, CancellationToken cancellationToken)
        {
            Cinema cinema = await _uow.CinemaRepository.GetByIdAsync(command.CinemaId);
            if(cinema is null)
            {
                return Error.NotFound(description: "Cinema with such id does not exist");
            }
            
            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            if(command.Name is not null)
                cinema.Name = command.Name;

            if(command.Adress is not null)
                cinema.Adress = command.Adress;

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
