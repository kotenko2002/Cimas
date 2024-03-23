using Cimas.Api.Contracts.Auth;
using Cimas.Api.Contracts.Cinemas;
using Cimas.Api.Contracts.Films;
using Cimas.Api.Contracts.Halls;
using Cimas.Api.Contracts.Sessions;
using Cimas.Api.Contracts.Tickets;
using Cimas.Api.Contracts.Users;
using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Films.Commands.CreateFilm;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Application.Features.Halls.Commands.UpdateHallSeats;
using Cimas.Application.Features.Sessions.Commands.CreateSession;
using Cimas.Application.Features.Sessions.Queries.GetSessionsByRange;
using Cimas.Application.Features.Tickets.Commands.CreateTickets;
using Cimas.Application.Features.Tickets.Commands.UpdateTickets;
using Cimas.Domain.Entities.Sessions;
using Mapster;

namespace Cimas.Api.Common.Mapping
{
    public static class ControllerMappingConfig
    {
        public static TypeAdapterConfig AddControllerMappingConfig(this TypeAdapterConfig config)
        {
            config
                .AddAuthControllerConfig()
                .AddCinemaControllerConfig()
                .AddFilmControllerConfig()
                .AddHallControllerConfig()
                .AddSessionControllerConfig()
                .AddTicketControllerConfig();
                //.AddUserControllerConfig();

            return config;
        }

        private static TypeAdapterConfig AddAuthControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(string Role, RegisterRequest requset), RegisterCommand>()
                .Map(dest => dest.Role, src => src.Role)
                .Map(dest => dest, src => src.requset);

            return config;
        }

        private static TypeAdapterConfig AddCinemaControllerConfig(this TypeAdapterConfig config)
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

        private static TypeAdapterConfig AddFilmControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateFilmRequest requset), CreateFilmCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);

            return config;
        }

        private static TypeAdapterConfig AddHallControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateHallRequest requset), CreateHallCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, Guid HallId, UpdateHallSeatsRequst requset), UpdateHallSeatsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.HallId, src => src.HallId)
                .Map(dest => dest, src => src.requset);

            return config;
        }

        private static TypeAdapterConfig AddSessionControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateSessionRequest requset), CreateSessionCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, GetSessionsByRangeRequest requset), GetSessionsByRangeQuery>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<Session, SessionResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.StartDateTime, src => src.StartDateTime)
                .Map(dest => dest.EndDateTime, src => src.StartDateTime + src.Film.Duration)
                .Map(dest => dest.HallName, src => src.Hall.Name)
                .Map(dest => dest.FilmName, src => src.Film.Name);

            return config;
        }

        private static TypeAdapterConfig AddTicketControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid SessionId, CreateTicketsRequest requset), CreateTicketsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.SessionId, src => src.SessionId)
                .Map(dest => dest, src => src.requset);

            config.NewConfig<(Guid UserId, UpdateTicketsRequest requset), UpdateTicketsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.requset);

            return config;
        }

        //private static TypeAdapterConfig AddUserControllerConfig(this TypeAdapterConfig config)
        //{
        //    config.NewConfig<(Guid UserId, RegisterNonOwnerRequest requset), CreateUserCommand>()
        //        .Map(dest => dest.OwnerUserId, src => src.UserId)
        //        .Map(dest => dest, src => src.requset);

        //    return config;
        //}
    }
}
