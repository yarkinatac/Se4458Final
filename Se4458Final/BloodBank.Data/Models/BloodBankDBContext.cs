using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Data.Models
{
    public partial class BloodBankDBContext : DbContext
    {
        public BloodBankDBContext()
        {
        }

        public BloodBankDBContext(DbContextOptions<BloodBankDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hospital> Hospitals { get; set; } = null!;
        public virtual DbSet<RequestForBlood> BloodRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=tcp:blooddonorsystem.database.windows.net,1433;Initial Catalog=se4458finalyarkin;Persist Security Info=False;User ID=berkay;Password=admin123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("hospital");

                entity.Property(e => e.HospitalID).HasColumnName("hospital_id");

                entity.Property(e => e.AMinusBlood).HasColumnName("a_minus_blood");

                entity.Property(e => e.APlusBlood).HasColumnName("a_plus_blood");

                entity.Property(e => e.AbMinusBlood).HasColumnName("ab_minus_blood");

                entity.Property(e => e.AbPlusBlood).HasColumnName("ab_plus_blood");

                entity.Property(e => e.BMinusBlood).HasColumnName("b_minus_blood");

                entity.Property(e => e.BPlusBlood).HasColumnName("b_plus_blood");

                entity.Property(e => e.GeopointID).HasColumnName("geopoint_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ZeroMinusBlood).HasColumnName("zero_minus_blood_unit");

                entity.Property(e => e.ZeroPlusBlood).HasColumnName("zero_plus_blood_unit");
            });
            modelBuilder.Entity<RequestForBlood>(entity =>
            {
                entity.HasKey(e => e.RequestID);
                entity.ToTable("blood_request");

                entity.Property(e => e.RequestID).HasColumnName("id");
                entity.Property(e => e.BloodNumber).HasColumnName("blood_number");
                entity.Property(e => e.BloodType).HasColumnName("blood_type");

                entity.Property(e => e.DurationDate)
                    .HasColumnName("duration_time");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Town).HasColumnName("town");
                entity.Property(e => e.HospitalLongitude).HasColumnName("hospital_longitude");
                entity.Property(e => e.HospitalLatitude).HasColumnName("hospital_latitude");
                entity.Property(e => e.HospitalEmail).HasColumnName("hospital_email");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
