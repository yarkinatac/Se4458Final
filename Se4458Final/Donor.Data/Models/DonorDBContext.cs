using System;
using System.Collections.Generic;
using Donor.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Donor.Data.Models
{
    public partial class DonorDBContext : DbContext
    {
        public DonorDBContext()
        {
        }

        public DonorDBContext(DbContextOptions<DonorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<DonationHistory> DonationHistories { get; set; } = null!;
        public virtual DbSet<Donor> Donors { get; set; } = null!;

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
         
            modelBuilder.Entity<Donor>(entity =>
            {
                entity.ToTable("donor");

                entity.Property(e => e.DonorID).HasColumnName("donor_id");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(5)
                    .HasColumnName("blood_type");

                entity.Property(e => e.BranchID).HasColumnName("branch_id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.Town).HasColumnName("town");
            });

            modelBuilder.Entity<DonationHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryID); 

                entity.ToTable("donation_history");

                entity.Property(e => e.HistoryID).HasColumnName("history_id");
                entity.Property(e => e.DonationDate).HasColumnName("donation_time");
                entity.Property(e => e.DonorID).HasColumnName("donor_id");
                entity.Property(e => e.TupleNumber).HasColumnName("tuple_count");

            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("branch");

                entity.Property(e => e.BranchID).HasColumnName("branch_id");

                entity.Property(e => e.AMinusBlood).HasColumnName("a_minus_blood");

                entity.Property(e => e.APlusBlood).HasColumnName("a_plus_blood");

                entity.Property(e => e.AbMinusBlood).HasColumnName("ab_minus_blood");

                entity.Property(e => e.AbPlusBlood).HasColumnName("ab_plus_blood");

                entity.Property(e => e.BMinusBlood).HasColumnName("b_minus_blood");

                entity.Property(e => e.BPlusBlood).HasColumnName("b_plus_blood");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.GeopointID).HasColumnName("geopoint_id");

                entity.Property(e => e.Town).HasColumnName("town");

                entity.Property(e => e.ZeroMinusBlood).HasColumnName("zero_minus_blood");

                entity.Property(e => e.ZeroPlusBlood).HasColumnName("zero_plus_blood");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
