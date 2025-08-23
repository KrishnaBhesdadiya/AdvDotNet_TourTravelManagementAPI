using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TourTravel.Models;

public partial class TourManagementContext : DbContext
{
    public TourManagementContext()
    {
    }

    public TourManagementContext(DbContextOptions<TourManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Itinerary> Itineraries { get; set; }

    public virtual DbSet<MstCustomer> MstCustomers { get; set; }

    public virtual DbSet<MstDestination> MstDestinations { get; set; }

    public virtual DbSet<MstPackage> MstPackages { get; set; }

    public virtual DbSet<MstTraveler> MstTravelers { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    public virtual DbSet<PackageDestination> PackageDestinations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-TO5U8DE\\SQLEXPRESS01; database=TourManagement; trusted_connection=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK_MST_Bookings");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingCode).HasMaxLength(50);
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BookingStatus).HasMaxLength(50);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TravelStartDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MST_Customers");

            entity.HasOne(d => d.Package).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MST_Packages");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MST_Users");
        });

        modelBuilder.Entity<Itinerary>(entity =>
        {
            entity.HasKey(e => e.ItineraryId).HasName("PK_MST_Itineraries");

            entity.Property(e => e.ItineraryId).HasColumnName("ItineraryID");
            entity.Property(e => e.ActivityName).HasMaxLength(50);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DayNumber).HasMaxLength(50);
            entity.Property(e => e.LocationDetails).HasMaxLength(500);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Package).WithMany(p => p.Itineraries)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Itineraries_MST_Packages");

            entity.HasOne(d => d.User).WithMany(p => p.Itineraries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Itineraries_MST_Users");
        });

        modelBuilder.Entity<MstCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("MST_Customers");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.MstCustomers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Customers_MST_Users");
        });

        modelBuilder.Entity<MstDestination>(entity =>
        {
            entity.HasKey(e => e.DestinationId).HasName("PK_MST_Destination");

            entity.ToTable("MST_Destinations");

            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DestinationCode).HasMaxLength(50);
            entity.Property(e => e.DestinationName).HasMaxLength(100);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.MstDestinations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Destinations_MST_Users");
        });

        modelBuilder.Entity<MstPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId);

            entity.ToTable("MST_Packages");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.AvailabilityStatus).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.ExcludedFeatures).HasMaxLength(100);
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.IncludedFeatures).HasMaxLength(100);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PackageCode).HasMaxLength(50);
            entity.Property(e => e.PackageName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.MstPackages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Packages_MST_Users");
        });

        modelBuilder.Entity<MstTraveler>(entity =>
        {
            entity.HasKey(e => e.TravelerId);

            entity.ToTable("MST_Travelers");

            entity.Property(e => e.TravelerId).HasColumnName("TravelerID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.EmergencyContactName).HasMaxLength(50);
            entity.Property(e => e.EmergencyContactNumber).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.MstTravelers)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Travelers_Bookings");

            entity.HasOne(d => d.User).WithMany(p => p.MstTravelers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MST_Travelers_MST_Users");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("MST_Users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(10);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<PackageDestination>(entity =>
        {
            entity.HasKey(e => e.PackageDestinationId).HasName("PK_MST_PackageDestinations");

            entity.Property(e => e.PackageDestinationId).HasColumnName("PackageDestinationID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Destination).WithMany(p => p.PackageDestinations)
                .HasForeignKey(d => d.DestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageDestinations_MST_Destinations");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageDestinations)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageDestinations_MST_Packages");

            entity.HasOne(d => d.User).WithMany(p => p.PackageDestinations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageDestinations_MST_Users");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_MST_Payments");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Bookings");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_MST_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
