using Microsoft.EntityFrameworkCore;
using post_office_back.Models;

namespace post_office_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }
        public DbSet<Shipment> Shipments { get; set; }
      
    }
}
