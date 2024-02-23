namespace Cimas.Domain.Auth
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
