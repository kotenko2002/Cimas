using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetAllCinemas
{
    public class GetAllCinemasQueryHandler : IRequestHandler<GetAllCinemasQuery, ErrorOr<List<Cinema>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetAllCinemasQueryHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<Cinema>>> Handle(GetAllCinemasQuery query, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(query.UserId.ToString());

            return await _uow.CinemaRepository.GetCinemasByCompanyIdAsync(user.CompanyId);
        }
    }
}
