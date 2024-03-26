using Cimas.Api.Contracts.Auth;
using Cimas.Api.Contracts.Cinemas;
using Cimas.Api.Contracts.Films;
using Cimas.Api.Contracts.Halls;
using Cimas.Api.Contracts.Products;
using Cimas.Api.Contracts.Sessions;
using Cimas.Api.Contracts.Tickets;
using Cimas.Application.Features.Auth.Commands.RegisterNonOwner;
using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Films.Commands.CreateFilm;
using Cimas.Application.Features.Halls.Commands.CreateHall;
using Cimas.Application.Features.Halls.Commands.UpdateHallSeats;
using Cimas.Application.Features.Products.Commands.CreateProduct;
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
                .AddCinemaControllerConfig()
                .AddFilmControllerConfig()
                .AddHallControllerConfig()
                .AddSessionControllerConfig()
                .AddTicketControllerConfig()
                .AddUserControllerConfig()
                .AddProductControllerConfig();

            return config;
        }

        private static TypeAdapterConfig AddCinemaControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateCinemaRequest Requset), CreateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Requset);

            config.NewConfig<(Guid UserId, Guid CinemaId, UpdateCinemaRequest Requset), UpdateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }

        private static TypeAdapterConfig AddFilmControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateFilmRequest Requset), CreateFilmCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }

        private static TypeAdapterConfig AddHallControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateHallRequest Requset), CreateHallCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.Requset);

            config.NewConfig<(Guid UserId, Guid HallId, UpdateHallSeatsRequst Requset), UpdateHallSeatsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.HallId, src => src.HallId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }

        private static TypeAdapterConfig AddSessionControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, CreateSessionRequest Requset), CreateSessionCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Requset);

            config.NewConfig<(Guid UserId, GetSessionsByRangeRequest Requset), GetSessionsByRangeQuery>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Requset);

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
            config.NewConfig<(Guid UserId, Guid SessionId, CreateTicketsRequest Requset), CreateTicketsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.SessionId, src => src.SessionId)
                .Map(dest => dest, src => src.Requset);

            config.NewConfig<(Guid UserId, UpdateTicketsRequest Requset), UpdateTicketsCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }

        private static TypeAdapterConfig AddUserControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, RegisterNonOwnerRequest Requset), RegisterNonOwnerCommand>()
                .Map(dest => dest.OwnerUserId, src => src.UserId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }

        private static TypeAdapterConfig AddProductControllerConfig(this TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, CreateProductRequest Requset), CreateProductCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.Requset);

            return config;
        }
    }
}
