using EF.Models;
using System.Data.Entity;

namespace EF
{
    public class DataModel : DbContext
    {
        public DbSet<City> Cities { get; set; }
    }
}
