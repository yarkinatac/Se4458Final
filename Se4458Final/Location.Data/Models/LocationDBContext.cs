using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Location.Data.Models
{
    public partial class LocationDBContext : DbContext
    {
        public LocationDBContext()
        {
        }

        public LocationDBContext(DbContextOptions<LocationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Geopoint> Geopoints { get; set; } = null!;
        public virtual DbSet<Town> Towns { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:blooddonorsystem.database.windows.net,1433;Initial Catalog=se4458finalyarkin;Persist Security Info=False;User ID=berkay;Password=admin123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.CityID)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.HasKey(e => e.CityID);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Geopoint>(entity =>
            {
                entity.ToTable("geopoint");

                entity.Property(e => e.GeopointID)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.ToTable("town");

                entity.Property(e => e.TownID).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
