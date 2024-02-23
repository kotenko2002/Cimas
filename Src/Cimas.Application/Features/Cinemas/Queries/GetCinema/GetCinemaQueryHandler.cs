using Cimas.Application.Interfaces;
using ErrorOr;
using MediatR;
using Cimas.Domain.Entities.Users;
using Cimas.Domain.Entities.Cinemas;

namespace Cimas.Application.Features.Cinemas.Queries.GetCinema
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
            User user = await _userManager.FindByIdAsync(query.UserId.ToString());

            Cinema cinema = await _uow.CinemaRepository.GetByIdAsync(query.CinemaId);
            if (cinema is null)
            {
                return Error.NotFound(description: "Cinema with such id does not exist");
            }

            if (user.CompanyId != cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            return cinema;
        }
    }
}
