using Cimas.Domain.Companies;
using Cimas.Domain.Films;
using Cimas.Domain.Halls;
using Cimas.Domain.Products;
using Cimas.Domain.WorkDays;

namespace Cimas.Domain.Cinemas
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
