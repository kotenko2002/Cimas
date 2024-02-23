using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
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
            var user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            return await _uow.CinemaRepository.GetCinemasByCompanyIdAsync(user.CompanyId);
        }
    }
}
