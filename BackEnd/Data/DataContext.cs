﻿using Microsoft.EntityFrameworkCore;
using post_office_back.Models;
using post_office_back.Models.Enums;

namespace post_office_back.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }
        public DbSet<Shipment> Shipments { get; set; }
        DbSet<Shipment> IDataContext.Shipments => Shipments;
        public DbSet<Bag> Bags { get; set; }
        DbSet<Bag> IDataContext.Bags => Bags;
        public DbSet<Parcel> Parcels { get; set; } = default!;
        DbSet<Parcel> IDataContext.Parcels => Parcels;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Bag>()
                    .Property(b => b.Weight)
                    .HasPrecision(18, 3);
            modelBuilder.Entity<Bag>()
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
               .Property(s => s.BagType)
               .HasConversion(
                   v => v.ToString(),
                   v => (BagType)Enum.Parse(typeof(BagType), v));

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
    }
}
