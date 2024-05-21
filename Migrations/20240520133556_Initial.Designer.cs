﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using post_office_back.Data;

#nullable disable

namespace post_office_back.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240520133556_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("post_office_back.Models.Bag", b =>
                {
                    b.Property<string>("BagNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShipmentNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BagNumber");

                    b.HasIndex("ShipmentNumber");

                    b.ToTable("Bags");
                });

            modelBuilder.Entity("post_office_back.Models.Shipment", b =>
                {
                    b.Property<string>("ShipmentNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DestinationAirport")
                        .HasColumnType("int");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("bit");

                    b.HasKey("ShipmentNumber");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("post_office_back.Models.Bag", b =>
                {
                    b.HasOne("post_office_back.Models.Shipment", null)
                        .WithMany("Bags")
                        .HasForeignKey("ShipmentNumber");
                });

            modelBuilder.Entity("post_office_back.Models.Shipment", b =>
                {
                    b.Navigation("Bags");
                });
#pragma warning restore 612, 618
        }
    }
}