using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Application.Features.Halls.Commands.UpdateHallSeats;
using Cimas.Api.Contracts.Cinemas;
using Cimas.Api.Contracts.Halls;
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

        private void AddCinemaControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateCinemaRequest requset), CreateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, Guid CinemaId, UpdateCinemaRequest requset), UpdateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);
        }

        private void AddHallControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateHallRequest requset), CreateHallCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, Guid HallId, UpdateHallSeatsRequst requset), UpdateHallSeatsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.HallId, src => src.HallId)
                .Map(dest => dest, src => src.requset);
        }
    }
}
