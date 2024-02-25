namespace Cimas.Application.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsSeatStatus(this int value)
        {
            return value >= 0 && value <= 2;
        }
    }
}
