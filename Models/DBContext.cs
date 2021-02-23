﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrighterCapAPI.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CountyData> CountyData { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Valuation> Valuation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=104.198.137.135;Database=MarketData;User Id=root;Password=admin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountyData>(entity =>
            {
                entity.HasKey(e => e.ParcelId)
                    .HasName("PRIMARY");

                entity.Property(e => e.ParcelId)
                    .HasColumnName("ParcelID")
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRecordHolder)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfListing).HasColumnType("datetime(6)");

                entity.Property(e => e.DefendantInFifa)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Ltv)
                    .HasColumnName("LTV")
                    .HasColumnType("decimal(10,3)");

                entity.Property(e => e.MailingAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfYears).HasColumnType("int(11)");

                entity.Property(e => e.PropertyAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyClass)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TaxYearsDue)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.TaxesOwed).HasColumnType("decimal(10,3)");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => new { e.ParcelId, e.SaleDate })
                    .HasName("PRIMARY");

                entity.Property(e => e.ParcelId)
                    .HasColumnName("ParcelID")
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.SaleDate).HasColumnType("datetime(6)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(13,3)");
            });

            modelBuilder.Entity<Valuation>(entity =>
            {
                entity.HasKey(e => new { e.ParcelId, e.ValuationDate })
                    .HasName("PRIMARY");

                entity.Property(e => e.ParcelId)
                    .HasColumnName("ParcelID")
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ValuationDate).HasColumnType("datetime(6)");

                entity.Property(e => e.AppraisedValue).HasColumnType("decimal(10,3)");

                entity.Property(e => e.BuildingValue).HasColumnType("decimal(10,3)");

                entity.Property(e => e.LandValue).HasColumnType("decimal(10,3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
