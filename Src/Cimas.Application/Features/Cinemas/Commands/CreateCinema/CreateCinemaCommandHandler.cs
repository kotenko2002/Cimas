using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Commands.CreateCinema
{
    public class CreateCinemaCommandHandler : IRequestHandler<CreateCinemaCommand, ErrorOr<Cinema>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public CreateCinemaCommandHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Cinema>> Handle(CreateCinemaCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
     
            var cinema = new Cinema()
            {
                Id = Guid.NewGuid(),
                CompanyId = user.CompanyId,
                Name = command.Name,
                Adress = command.Adress
            };

            await _uow.CinemaRepository.AddAsync(cinema);
            await _uow.CompleteAsync();

            return cinema;
        }
    }
}
