using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetCinemaById
{
    public class GetCinemaQueryHandler : IRequestHandler<GetCinemaQuery, ErrorOr<Cinema>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetCinemaQueryHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Cinema>> Handle(GetCinemaQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            var cinema = await _uow.CinemaRepository.GetByIdAsync(query.CinemaId);
            if (cinema is null)
            {
                return Error.NotFound(description: "Cinema with such id does not exist");
            }

            if (user.CompanyId != cinema.CompanyId)
            {
                return Error.Unauthorized(description: "You do not have the necessary permissions to perform this action");
            }

            return cinema;
        }
    }
}
