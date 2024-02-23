namespace Cimas.Domain.Users
{
    public static class Roles
    {
        public const string Owner = "Owner";
        public const string Worker = "Worker";
        public const string Reviewer = "Reviewer";

        public static string[] GetRoles()
        {
            return [Owner, Worker, Reviewer];
        }
    }
}
