namespace Cimas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        ICinemaRepository CinemaRepository { get; }
        IHallRepository HallRepository { get; }
        ISeatRepository SeatRepository { get; }
        IFilmRepository FilmRepository { get; }
        ISessionRepository SessionRepository { get; }
        ITicketRepository TicketRepository { get; }
        IUserRepository UserRepository { get; }

        Task CompleteAsync();
    }
}
