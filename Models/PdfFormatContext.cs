﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomPdf_BE.Models;

public partial class PdfFormatContext : DbContext
{
    public PdfFormatContext()
    {
    }

    public PdfFormatContext(DbContextOptions<PdfFormatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhXa> AnhXas { get; set; }

    public virtual DbSet<Cot> Cots { get; set; }

    public virtual DbSet<DauCham> DauChams { get; set; }

    public virtual DbSet<LoaiThuocTinh> LoaiThuocTinhs { get; set; }

    public virtual DbSet<MauPdf> MauPdfs { get; set; }

    public virtual DbSet<Ovuong> Ovuongs { get; set; }

    public virtual DbSet<ThuocTinh> ThuocTinhs { get; set; }

    public virtual DbSet<ThuocTinhMau> ThuocTinhMaus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PdfFormat;User Id=sa;Password=123456aA@$;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhXa>(entity =>
        {
            entity.HasKey(e => e.IdAnhXa).HasName("PK__AnhXa__1ACD116D7388B642");

            entity.ToTable("AnhXa");

            entity.Property(e => e.TenCot)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenThuocTinh).HasMaxLength(50);
        });

        modelBuilder.Entity<Cot>(entity =>
        {
            entity.HasKey(e => e.IdCot).HasName("PK__Cot__0FA7F2935B365E95");

            entity.ToTable("Cot");

            entity.HasIndex(e => e.IdCot, "UQ__Cot__0FA7F292F237BEFC").IsUnique();

            entity.Property(e => e.TenCot).HasMaxLength(255);

            entity.HasOne(d => d.IdThuocTinhNavigation).WithMany(p => p.Cots)
                .HasForeignKey(d => d.IdThuocTinh)
                .HasConstraintName("FK_Cot_ThuocTinh");
        });

        modelBuilder.Entity<DauCham>(entity =>
        {
            entity.HasKey(e => e.IdThuocTinh).HasName("PK__tmp_ms_x__A9375578D2EDDF09");

            entity.ToTable("DauCham");

            entity.Property(e => e.IdThuocTinh).ValueGeneratedNever();

            entity.HasOne(d => d.IdThuocTinhNavigation).WithOne(p => p.DauCham)
                .HasForeignKey<DauCham>(d => d.IdThuocTinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DauCham_ThuocTinh");
        });

        modelBuilder.Entity<LoaiThuocTinh>(entity =>
        {
            entity.HasKey(e => e.IdLoai).HasName("PK__LoaiThuo__38DBC330125032BB");

            entity.ToTable("LoaiThuocTinh");

            entity.HasIndex(e => e.IdLoai, "UQ__LoaiThuo__38DBC33120905609").IsUnique();

            entity.Property(e => e.TenLoai).HasMaxLength(255);
        });

        modelBuilder.Entity<MauPdf>(entity =>
        {
            entity.HasKey(e => e.IdMau).HasName("PK__MauPDF__0D13B7451EAD4AF6");

            entity.ToTable("MauPDF");

            entity.HasIndex(e => e.IdMau, "UQ__MauPDF__0D13B744B45F8A45").IsUnique();

            entity.Property(e => e.Dai).HasComment("Đơn vị (mm)");
            entity.Property(e => e.Rong).HasComment("Đơn vị (mm)");
            entity.Property(e => e.TenMau).HasMaxLength(255);
        });

        modelBuilder.Entity<Ovuong>(entity =>
        {
            entity.HasKey(e => e.IdThuocTinh).HasName("PK__OVuong__A93755784E0FBDED");

            entity.ToTable("OVuong");

            entity.Property(e => e.IdThuocTinh).ValueGeneratedNever();
            entity.Property(e => e.Rong)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdThuocTinhNavigation).WithOne(p => p.Ovuong)
                .HasForeignKey<Ovuong>(d => d.IdThuocTinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OVuong_ThuocTinh");
        });

        modelBuilder.Entity<ThuocTinh>(entity =>
        {
            entity.HasKey(e => e.IdThuocTinh);

            entity.ToTable("ThuocTinh");

            entity.HasIndex(e => e.IdThuocTinh, "UQ__tmp_ms_x__A937557961BA96DC").IsUnique();

            entity.Property(e => e.NoiDung).HasMaxLength(255);

            entity.HasOne(d => d.IdLoaiNavigation).WithMany(p => p.ThuocTinhs)
                .HasForeignKey(d => d.IdLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThuocTinh__IdLoa__29221CFB");
        });

        modelBuilder.Entity<ThuocTinhMau>(entity =>
        {
            entity.HasKey(e => new { e.IdThuocTinh, e.IdMau }).HasName("PK__tmp_ms_x__E9E66E0C3A8A10A3");

            entity.ToTable("ThuocTinh_Mau");

            entity.HasIndex(e => e.IdThuocTinh, "UQ__tmp_ms_x__A937557988EF0A22").IsUnique();

            entity.HasOne(d => d.IdMauNavigation).WithMany(p => p.ThuocTinhMaus)
                .HasForeignKey(d => d.IdMau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThuocTinh__IdMau__5165187F");

            entity.HasOne(d => d.IdThuocTinhNavigation).WithOne(p => p.ThuocTinhMau)
                .HasForeignKey<ThuocTinhMau>(d => d.IdThuocTinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThuocTinh_Mau_ThuocTinh");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
