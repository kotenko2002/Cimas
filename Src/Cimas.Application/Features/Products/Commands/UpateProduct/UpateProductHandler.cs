using Cimas.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Products.Commands.UpateProduct
{
    public class UpateProductHandler : IRequestHandler<UpateProductCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;

        public UpateProductHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<ErrorOr<Success>> Handle(UpateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
