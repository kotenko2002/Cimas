using Cimas.Domain.Entities.Companies;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Products;
using Cimas.Domain.Entities.WorkDays;

namespace Cimas.Domain.Entities.Cinemas
{
    public class Cinema : BaseEntity
    {
        public string Name { get; set; }
        public string Adress { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<WorkDay> WorkDays { get; set; }
    }
}
