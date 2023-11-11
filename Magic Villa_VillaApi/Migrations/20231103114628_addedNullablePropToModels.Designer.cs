﻿// <auto-generated />
using System;
using Magic_Villa_VillaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Magic_Villa_VillaApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231103114628_addedNullablePropToModels")]
    partial class addedNullablePropToModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Magic_Villa_VillaApi.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7682),
                            Details = "Some default details",
                            ImageUrl = "",
                            Name = "Royal Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 550,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7738),
                            Details = "Some default diamond villa details",
                            ImageUrl = "",
                            Name = "Diamond Villa",
                            Occupancy = 6,
                            Rate = 300.0,
                            Sqft = 650,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7742),
                            Details = "Some default ukrainian villa details",
                            ImageUrl = "",
                            Name = "Ukrainian Villa",
                            Occupancy = 7,
                            Rate = 400.0,
                            Sqft = 750,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Magic_Villa_VillaApi.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("VillaNumbers");

                    b.HasData(
                        new
                        {
                            VillaNo = 100,
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7922),
                            SpecialDetails = "Villa number is 100",
                            UpdatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7925),
                            VillaId = 0
                        },
                        new
                        {
                            VillaNo = 101,
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7928),
                            SpecialDetails = "Villa number is 101",
                            UpdatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7930),
                            VillaId = 0
                        },
                        new
                        {
                            VillaNo = 102,
                            CreatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7933),
                            SpecialDetails = "Villa number is 102",
                            UpdatedDate = new DateTime(2023, 11, 3, 13, 46, 28, 533, DateTimeKind.Local).AddTicks(7935),
                            VillaId = 0
                        });
                });

            modelBuilder.Entity("Magic_Villa_VillaApi.Models.VillaNumber", b =>
                {
                    b.HasOne("Magic_Villa_VillaApi.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}