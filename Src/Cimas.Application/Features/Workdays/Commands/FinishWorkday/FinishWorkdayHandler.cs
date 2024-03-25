using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.WorkDays;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Workdays.Commands.FinishWorkday
{
    public class FinishWorkdayHandler : IRequestHandler<FinishWorkdayCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;

        public FinishWorkdayHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ErrorOr<Success>> Handle(FinishWorkdayCommand command, CancellationToken cancellationToken)
        {
            Workday unfinishedWorkday =  await _uow.WorkdayRepository.GetWorkdayByUserId(command.UserId);
            if(unfinishedWorkday is null)
            {
                return Error.Failure(description: "User does not have an unfinished workday");
            }

            unfinishedWorkday.EndDateTime = DateTime.UtcNow;

            // TODO: impl logic of generating reports

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
