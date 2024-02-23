namespace Cimas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        ICinemaRepository CinemaRepository { get; }

        Task CompleteAsync();
    }
}
