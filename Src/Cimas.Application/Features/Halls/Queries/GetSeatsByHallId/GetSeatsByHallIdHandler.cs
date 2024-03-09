using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Halls.Queries.GetSeatsByHallId
{
    public class GetSeatsByHallIdHandler : IRequestHandler<GetSeatsByHallIdQuery, ErrorOr<List<HallSeat>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetSeatsByHallIdHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<HallSeat>>> Handle(GetSeatsByHallIdQuery query, CancellationToken cancellationToken)
        {
            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaByIdAsync(query.HallId);
            if (hall is null)
            {
                return Error.NotFound(description: "Hall with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            return await _uow.SeatRepository.GetSeatsByHallId(hall.Id);
        }
    }
}
