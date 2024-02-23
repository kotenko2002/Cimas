using Cimas.Domain.Cinemas;
using Cimas.Domain.Users;

namespace Cimas.Domain.Companies
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
