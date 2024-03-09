namespace Cimas.Domain.Models.Sessions
{
    public class SessionSeat
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public SessionSeatStatus Status { get; set; }
    }
}
