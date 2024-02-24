using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Contracts.Cinemas;
using Cimas.Contracts.Halls;
using Mapster;

namespace Cimas.Api.Common.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            AddCinemaControllerConfigs(config);
            AddHallControllerConfigs(config);
        }

        private TypeAdapterConfig AddCinemaControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateCinemaRequest requset), CreateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, Guid CinemaId, UpdateCinemaRequest requset), UpdateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);

            return config;
        }

        private TypeAdapterConfig AddHallControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateHallRequest requset), CreateHallCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            return config;
        }
    }
}
