using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Products.Commands.UpateProduct
{
    public record UpateProductCommand() : IRequest<ErrorOr<Success>>; // many
}
