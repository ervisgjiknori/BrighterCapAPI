using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=35.232.153.216;Database=MarketData;User Id=sqlserver;Password=Dev2021!;MultipleActiveResultSets=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CountyData>(entity =>
            {
                entity.HasKey(e => e.ParcelId)
                    .HasName("PK__CountyDa__B5F2165BCD3517C8");

                entity.Property(e => e.ParcelId)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("ParcelID");

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

                entity.Property(e => e.DefendantInFifa)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Ltv)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("LTV");

                entity.Property(e => e.MailingAddress)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyAddress)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyClass)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.TaxYearsDue)
                    .IsRequired()
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.TaxesOwed).HasColumnType("decimal(10, 3)");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => new { e.ParcelId, e.SaleDate });

                entity.Property(e => e.ParcelId)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("ParcelID");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 3)");
            });

            modelBuilder.Entity<Valuation>(entity =>
            {
                entity.HasKey(e => new { e.ParcelId, e.ValuationDate });

                entity.Property(e => e.ParcelId)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("ParcelID");

                entity.Property(e => e.AppraisedValue).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.BuildingValue).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.LandValue).HasColumnType("decimal(10, 3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
