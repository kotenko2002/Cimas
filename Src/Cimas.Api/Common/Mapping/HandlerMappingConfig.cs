using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Features.Users.Commands.RegisterNonOwner;
using Cimas.Application.Features.Users.Commands.RegisterOwner;
using Cimas.Domain.Entities.Companies;
using Mapster;

namespace Cimas.Api.Common.Mapping
{
    public static class HandlerMappingConfig
    {
        public static TypeAdapterConfig AddHandlerMappingConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Company Company, string Role, RegisterOwnerCommand Requset), RegisterCommand>()
                .Map(dest => dest.Company, src => src.Company)
                .Map(dest => dest.Role, src => src.Role)
                .Map(dest => dest, src => src.Requset);

            config.NewConfig<(Company Company, string Role, RegisterNonOwnerCommand Requset), RegisterCommand>()
                .Map(dest => dest.Company, src => src.Company)
                .Map(dest => dest, src => src.Requset);

            return config;
        }
    }
}
