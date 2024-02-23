using Cimas.Domain.Companies;
using Cimas.Domain.WorkDays;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Domain.Users
{
    public class User : IdentityUser<Guid>
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool IsFired { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<WorkDay> WorkDays { get; set; }
    }
}
