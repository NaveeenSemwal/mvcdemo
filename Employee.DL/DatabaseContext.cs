using Employees.DL.Database;
using System.Data.Entity;

namespace Employees.DL
{
    public class EMSDemoEntities : DbContext
    {
        public EMSDemoEntities()
            : base("EMSDemoEntities")
        {
        }

        public virtual DbSet<TitleMaster> TitleMasters { get; set; }
        public virtual DbSet<CountriesMaster> CountriesMasters { get; set; }
        public virtual DbSet<Database.Employee> Employees { get; set; }
    }
}
