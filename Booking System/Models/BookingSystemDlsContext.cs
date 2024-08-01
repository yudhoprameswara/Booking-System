using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Booking_System.Models;

public partial class BookingSystemDlsContext : DbContext
{
    public BookingSystemDlsContext()
    {
    }

    public BookingSystemDlsContext(DbContextOptions<BookingSystemDlsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstBooking> MstBookings { get; set; }

    public virtual DbSet<MstLocation> MstLocations { get; set; }

    public virtual DbSet<MstMenu> MstMenus { get; set; }

    public virtual DbSet<MstResource> MstResources { get; set; }

    public virtual DbSet<MstResourceCode> MstResourceCodes { get; set; }

    public virtual DbSet<MstRole> MstRoles { get; set; }

    public virtual DbSet<MstRoom> MstRooms { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    public virtual DbSet<Rolemenu> Rolemenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookingSystemDLS;Username=postgres;Password=indocyber");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstBooking_pkey");

            entity.ToTable("MstBooking");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.BookCode).HasColumnType("character varying");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<MstLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstLocation_pkey");

            entity.ToTable("MstLocation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MstMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstMenu_pkey");

            entity.ToTable("MstMenu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MstResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstResource_pkey");

            entity.ToTable("MstResource");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ResouceName).HasMaxLength(200);
            entity.Property(e => e.ResourceIcon).HasMaxLength(100000);
            entity.Property(e => e.UpdateDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<MstResourceCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstResource Code_pkey");

            entity.ToTable("MstResource Code");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ResourceCode).HasColumnType("character varying");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Resource).WithMany(p => p.MstResourceCodes)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MstResource Code_ResourceId_fkey");
        });

        modelBuilder.Entity<MstRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("MstRole");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasPrecision(6);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");
        });

        modelBuilder.Entity<MstRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("Room_pkey");

            entity.ToTable("MstRoom");

            entity.Property(e => e.RoomId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DelDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LocationOffice).HasMaxLength(500);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.RoomColor).HasMaxLength(500);
            entity.Property(e => e.RoomName).HasColumnType("character varying");
            entity.Property(e => e.UpdateDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Resource).WithMany(p => p.MstRooms)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("MstRoom_ResourceID_fkey");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("MstUser");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LoginName)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_createdby");

            entity.HasOne(d => d.DelByNavigation).WithMany(p => p.InverseDelByNavigation)
                .HasForeignKey(d => d.DelBy)
                .HasConstraintName("fk_deletedby");

            entity.HasOne(d => d.Role).WithMany(p => p.MstUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("RoleId");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_updatedby");
        });

        modelBuilder.Entity<Rolemenu>(entity =>
        {
            entity.HasKey(e => new { e.Roleid, e.Menuid }).HasName("rolemenu_pkey");

            entity.ToTable("rolemenu");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Menuid).HasColumnName("menuid");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.Menu).WithMany(p => p.Rolemenus)
                .HasForeignKey(d => d.Menuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolemenu_menuid_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolemenus)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolemenu_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
