using Microsoft.EntityFrameworkCore;
using post_office_back.Models;
using post_office_back.Models.Enums;
using System.Reflection.Metadata;

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
                .HasValue<LetterBag>("LETTERBAG")
                .HasValue<ParcelBag>("PARCELBAG");


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
            modelBuilder.Entity<Shipment>()
                .Property(s => s.DestinationAirport)
                .HasConversion(
                    v => v.ToString(),
                    v => (Airport)Enum.Parse(typeof(Airport), v));

            modelBuilder.Entity<Bag>()
                .HasOne(b => b.Shipment)
                .WithMany(s => s.Bags)
                .HasForeignKey(b => b.ShipmentNumber);

            modelBuilder.Entity<Parcel>()
                .HasOne(b => b.ParcelBag)
                .WithMany(s => s.Parcels)
                .HasForeignKey(b => b.ParcelBagBagNumber);


            base.OnModelCreating(modelBuilder);


        }
        public DbSet<Parcel> Parcel { get; set; } = default!;
    }
}
