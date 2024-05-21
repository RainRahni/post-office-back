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
        public DbSet<Bag> Bags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bag>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<LetterBag>("LetterBag")
                .HasValue<ParcelBag>("ParcelBag");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LetterBag>()
                    .Property(b => b.Weight)
                    .HasPrecision(18, 3);
            modelBuilder.Entity<LetterBag>()
                    .Property(b => b.Price)
                    .HasPrecision(18, 2);
            modelBuilder.Entity<Parcel>()
                    .Property(p => p.Weight)
                    .HasPrecision(18, 3);
            modelBuilder.Entity<Parcel>()
                    .Property(p => p.Price)
                    .HasPrecision(18, 2);
        }
    }
}
