namespace Cimas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        ICinemaRepository CinemaRepository { get; }
        IHallRepository HallRepository { get; }
        ISeatRepository SeatRepository { get; }
        IFilmRepository FilmRepository { get; }

        Task CompleteAsync();
    }
}
