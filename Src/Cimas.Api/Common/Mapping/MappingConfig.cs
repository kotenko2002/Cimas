﻿using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Application.Features.Halls.Commands.UpdateHallSeats;
using Cimas.Api.Contracts.Cinemas;
using Cimas.Api.Contracts.Halls;
using Mapster;
using Cimas.Application.Features.Films.Commands.CreateFilm;
using Cimas.Api.Contracts.Films;
using Cimas.Api.Contracts.Sessions;
using Cimas.Application.Features.Tickets.Commands.CreateTicket;
using Cimas.Api.Contracts.Tickets;
using Cimas.Application.Features.Sessions.Commands.CreateSession;

namespace Cimas.Api.Common.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            AddCinemaControllerConfigs(config);
            AddFilmControllerConfigs(config);
            AddHallControllerConfigs(config);
            AddSessionControllerConfigs(config);
            AddTicketControllerConfigs(config);
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

        private void AddFilmControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateFilmRequest requset), CreateFilmCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);
        }

        private void AddHallControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateHallRequest requset), CreateHallCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, Guid HallId, UpdateHallSeatsRequst requset), UpdateHallSeatsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.HallId, src => src.HallId)
                .Map(dest => dest, src => src.requset);
        }

        private void AddSessionControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateSessionRequest requset), CreateSessionCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);
        }

        private void AddTicketControllerConfigs(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateTicketRequest requset), CreateTicketCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);
        }
    }
}
