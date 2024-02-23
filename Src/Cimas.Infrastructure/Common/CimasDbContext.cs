using Cimas.Domain.Cinemas;
using Cimas.Domain.Companies;
using Cimas.Domain.Films;
using Cimas.Domain.Halls;
using Cimas.Domain.Products;
using Cimas.Domain.Reports;
using Cimas.Domain.Sessions;
using Cimas.Domain.Users;
using Cimas.Domain.WorkDays;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Common
{
    public class CimasDbContext
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Report> Reports { get; set; }

        public CimasDbContext(DbContextOptions<CimasDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>(builder =>
            {
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Adress).IsRequired();

                builder
                    .HasOne(c => c.Company)
                    .WithMany(c => c.Cinemas)
                    .HasForeignKey(c => c.CompanyId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(c => c.IsFired).IsRequired();

                builder
                    .HasOne(u => u.Company)
                    .WithMany(c => c.Users)
                    .HasForeignKey(u => u.CompanyId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Price)
                    .HasPrecision(18, 2)
                    .IsRequired();

                builder.Property(c => c.Amount).IsRequired();
                builder.Property(c => c.SoldAmount).IsRequired();
                builder.Property(c => c.IncomeAmount).IsRequired();

                builder.Property(c => c.IsDeleted).IsRequired();

                builder
                    .HasOne(p => p.Cinema)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CinemaId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Hall>(builder =>
            {
                builder.Property(c => c.Name).IsRequired();

                builder.Property(c => c.IsDeleted).IsRequired();

                builder
                    .HasOne(h => h.Cinema)
                    .WithMany(c => c.Halls)
                    .HasForeignKey(h => h.CinemaId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Film>(builder =>
            {
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Duration).IsRequired();

                builder.Property(c => c.IsDeleted).IsRequired();

                builder
                    .HasOne(f => f.Cinema)
                    .WithMany(c => c.Films)
                    .HasForeignKey(f => f.CinemaId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Seat>(builder =>
            {
                builder.Property(c => c.Number).IsRequired();
                builder.Property(c => c.Row).IsRequired();
                builder.Property(c => c.Column).IsRequired();
                builder.Property(c => c.Status).IsRequired();

                builder
                    .HasOne(s => s.Hall)
                    .WithMany(h => h.Seats)
                    .HasForeignKey(s => s.HallId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Session>(builder =>
            {
                builder.Property(c => c.StartTime).IsRequired();

                builder
                    .HasOne(s => s.Hall)
                    .WithMany(h => h.Sessions)
                    .HasForeignKey(s => s.HallId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .HasOne(s => s.Film)
                    .WithMany(f => f.Sessions)
                    .HasForeignKey(s => s.FilmId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Ticket>(builder =>
            {
                builder.Property(c => c.CreationTime).IsRequired();

                builder
                    .HasOne(t => t.Seat)
                    .WithMany(s => s.Tickets)
                    .HasForeignKey(t => t.SeatId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .HasOne(t => t.Session)
                    .WithMany(s => s.Tickets)
                    .HasForeignKey(t => t.SessionId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<WorkDay>(builder =>
            {
                builder.Property(c => c.StartDateTime).IsRequired();

                builder
                    .HasOne(w => w.Cinema)
                    .WithMany(c => c.WorkDays)
                    .HasForeignKey(w => w.CinemaId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .HasOne(w => w.User)
                    .WithMany(u => u.WorkDays)
                    .HasForeignKey(w => w.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Report>(builder =>
            {
                builder.Property(c => c.Status).IsRequired();

                builder
                    .HasOne(r => r.WorkDay)
                    .WithOne(wd => wd.Report)
                    .HasForeignKey<Report>(r => r.WorkDayId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
