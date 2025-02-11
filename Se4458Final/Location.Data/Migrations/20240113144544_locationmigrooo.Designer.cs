﻿// <auto-generated />
using Location.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Location.Data.Migrations
{
    [DbContext(typeof(LocationDBContext))]
    [Migration("20240113144544_locationmigrooo")]
    partial class locationmigrooo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Location.Data.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("CityID");

                    b.ToTable("city", (string)null);
                });

            modelBuilder.Entity("Location.Data.Models.Geopoint", b =>
                {
                    b.Property<int>("GeopointID")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasColumnName("longitude");

                    b.HasKey("GeopointID");

                    b.ToTable("geopoint", (string)null);
                });

            modelBuilder.Entity("Location.Data.Models.Town", b =>
                {
                    b.Property<int>("TownID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TownID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("name");

                    b.HasKey("TownID");

                    b.ToTable("town", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
