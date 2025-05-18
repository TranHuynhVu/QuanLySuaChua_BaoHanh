using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;


    public partial class BHSC_DbContext : IdentityDbContext<NguoiDung, IdentityRole<string>,string>
{
    public BHSC_DbContext()
    {
    }

    public BHSC_DbContext(DbContextOptions<BHSC_DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPn> ChiTietPns { get; set; }

    public virtual DbSet<ChiTietPx> ChiTietPxes { get; set; }

    public virtual DbSet<ChiTietSuaChua> ChiTietSuaChuas { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<LinhKien> LinhKiens { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuSuaChua> PhieuSuaChuas { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<Phuong> Phuongs { get; set; }

    public virtual DbSet<Quan> Quans { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<ThanhPho> ThanhPhos { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<TinNhan> TinNhans { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        //modelBuilder.Entity<IdentityUser>(b =>
        //{
        //    b.ToTable("NguoiDung");
        //    b.HasKey(u => u.Id);
        //    b.Property(u => u.Id).HasColumnName("NguoiDungID");
        //    b.Property(u => u.UserName).HasColumnName("TaiKhoan");
        //    b.Property(u => u.PasswordHash).HasColumnName("MatKhau");
        //    b.Property(u => u.Email).HasColumnName("Email");
        //    b.Property(u => u.PhoneNumber).HasColumnName("Sdt");
        //});

            modelBuilder.Entity<NguoiDung>(b =>
            {
                b.ToTable("NguoiDung");
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("NguoiDungID").ValueGeneratedOnAdd();
                b.Property(u => u.UserName).HasColumnName("TaiKhoan");
                b.Property(u => u.PasswordHash).HasColumnName("MatKhau");
                b.Property(u => u.Email).HasColumnName("Email");
                b.Property(u => u.PhoneNumber).HasColumnName("Sdt");
                b.Property(u => u.HoTen).HasColumnName("HoTen");
                b.Property(u => u.PhuongId).HasColumnName("PhuongID");
                b.Property(u => u.DiaChi).HasColumnName("DiaChi");
        modelBuilder.Entity<NguoiDung>(b =>
        {
            b.ToTable("NguoiDung");
            b.HasKey(u => u.Id);
            b.Property(u => u.Id).HasColumnName("NguoiDungID").ValueGeneratedOnAdd();
            b.Property(u => u.UserName).HasColumnName("TaiKhoan");
            b.Property(u => u.PasswordHash).HasColumnName("MatKhau");
            b.Property(u => u.Email).HasColumnName("Email");
            b.Property(u => u.PhoneNumber).HasColumnName("Sdt");
            b.Property(u => u.HoTen).HasColumnName("HoTen");
            b.Property(u => u.PhuongId).HasColumnName("PhuongID");
            b.Property(u => u.DiaChi).HasColumnName("DiaChi");
            b.HasOne(u => u.Phuong)
                                .WithMany(p => p.NguoiDungs)
                                .HasForeignKey(u => u.PhuongId);
        });

        modelBuilder.Entity<IdentityRole<string>>(b =>
        {
            b.ToTable("AspNetRoles");
            b.Property(r => r.Id)
                .ValueGeneratedNever(); // ID được cung cấp bởi code, không tự sinh
        });

            modelBuilder.Entity<IdentityRole<string>>(b =>
            {
                b.ToTable("AspNetRoles");
                b.Property(r => r.Id)
                    .ValueGeneratedNever(); // ID được cung cấp bởi code, không tự sinh
            });
                


        modelBuilder.Entity<ChiTietPn>(entity =>
            {
                entity.HasKey(e => new { e.PhieuNhapId, e.LinhKienId }).HasName("PK__ChiTietP__CE785146FEE0EA2E");

                entity.HasOne(d => d.LinhKien).WithMany(p => p.ChiTietPns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPN__LinhK__6B24EA82");

                entity.HasOne(d => d.PhieuNhap).WithMany(p => p.ChiTietPns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPN__Phieu__6A30C649");
            });

            modelBuilder.Entity<ChiTietPx>(entity =>
            {
                entity.HasKey(e => new { e.PhieuXuatId, e.PhieuSuaChuaId }).HasName("PK__ChiTietP__5169BB2B3005A347");

                entity.HasOne(d => d.PhieuSuaChua).WithMany(p => p.ChiTietPxes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPX__Phieu__6754599E");

                entity.HasOne(d => d.PhieuXuat).WithMany(p => p.ChiTietPxes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPX__Phieu__66603565");
            });

            modelBuilder.Entity<ChiTietSuaChua>(entity =>
            {
                entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietS__B117E9EAF0334BA5");

                entity.Property(e => e.ChiTietId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.LinhKien).WithMany(p => p.ChiTietSuaChuas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietSu__LinhK__70DDC3D8");

                entity.HasOne(d => d.PhieuSuaChua).WithMany(p => p.ChiTietSuaChuas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietSu__Phieu__6FE99F9F");

                entity.HasOne(d => d.SanPham).WithMany(p => p.ChiTietSuaChuas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietSu__SanPh__6EF57B66");
            });

            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.HasKey(e => e.DanhMucId).HasName("PK__DanhMuc__1C53BA7B3EE29315");
                entity.Property(e => e.DanhMucId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LinhKien>(entity =>
            {
                entity.HasKey(e => e.LinhKienId).HasName("PK__LinhKien__04269C40A0329C9C");

                entity.Property(e => e.LinhKienId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.DanhMuc).WithMany(p => p.LinhKiens).HasConstraintName("FK__LinhKien__DanhMu__47DBAE45");
            });

            

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.PhieuNhapId).HasName("PK__PhieuNha__DE3A3882C1EFE80A");

                entity.Property(e => e.PhieuNhapId).ValueGeneratedOnAdd();
                entity.Property(e => e.NgayNhap).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Kho).WithMany(p => p.PhieuNhaps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuNhap__KhoID__5AEE82B9");
            });

            modelBuilder.Entity<PhieuSuaChua>(entity =>
            {
                entity.HasKey(e => e.PhieuSuaChuaId).HasName("PK__PhieuSua__B46641E6385CDB55");

                entity.Property(e => e.PhieuSuaChuaId).ValueGeneratedOnAdd();

                entity.Property(e => e.NgayGui).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.KhachHang).WithMany(p => p.PhieuSuaChuaKhachHangs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuSuaC__Khach__5070F446");

                entity.HasOne(d => d.KyThuat).WithMany(p => p.PhieuSuaChuaKyThuats).HasConstraintName("FK__PhieuSuaC__KyThu__5165187F");

                entity.HasOne(d => d.Phuong).WithMany(p => p.PhieuSuaChuas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuSuaC__Phuon__52593CB8");
            });

            modelBuilder.Entity<PhieuXuat>(entity =>
            {
                entity.HasKey(e => e.PhieuXuatId).HasName("PK__PhieuXua__2A2FDF35F3666748");

                entity.Property(e => e.PhieuXuatId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.Kho).WithMany(p => p.PhieuXuats)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuXuat__KhoID__6383C8BA");
            });

            modelBuilder.Entity<Phuong>(entity =>
            {
                entity.HasKey(e => e.PhuongId).HasName("PK__Phuong__7FC46F50B400BD51");

                entity.Property(e => e.PhuongId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.Quan).WithMany(p => p.Phuongs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Phuong__QuanID__403A8C7D");
            });

            modelBuilder.Entity<Quan>(entity =>
            {
                entity.HasKey(e => e.QuanId).HasName("PK__Quan__B0ADAE910FF0D4CB");

                entity.Property(e => e.QuanId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.ThanhPho).WithMany(p => p.Quans)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quan__ThanhPhoID__3D5E1FD2");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.SanPhamId).HasName("PK__SanPham__05180FF4A990F499");

                entity.Property(e => e.SanPhamId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.DanhMuc).WithMany(p => p.SanPhams)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__DanhMuc__4BAC3F29");
                    
            entity.HasOne(d => d.LinhKien).WithMany(p => p.ChiTietPns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPN__LinhK__6B24EA82");

            modelBuilder.Entity<ThanhPho>(entity =>
            {
                entity.HasKey(e => e.ThanhPhoId).HasName("PK__ThanhPho__6E7123A00A09EC48");
                entity.Property(e => e.ThanhPhoId).ValueGeneratedOnAdd();
            });

        modelBuilder.Entity<ChiTietPx>(entity =>
        {
            entity.HasKey(e => new { e.PhieuXuatId, e.PhieuSuaChuaId }).HasName("PK__ChiTietP__5169BB2B3005A347");

                entity.Property(e => e.ThongBaoId).ValueGeneratedOnAdd();
                entity.Property(e => e.TrangThai).HasDefaultValue("ChuaDoc");

            entity.HasOne(d => d.PhieuXuat).WithMany(p => p.ChiTietPxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPX__Phieu__66603565");
        });

        modelBuilder.Entity<ChiTietSuaChua>(entity =>
        {
            entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietS__B117E9EAF0334BA5");

                entity.Property(e => e.TinNhanId).ValueGeneratedOnAdd();
                entity.HasOne(d => d.NguoiGui).WithMany(p => p.TinNhanNguoiGuis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TinNhan__NguoiGu__5EBF139D");

            entity.HasOne(d => d.PhieuSuaChua).WithMany(p => p.ChiTietSuaChuas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSu__Phieu__6FE99F9F");

            entity.HasOne(d => d.SanPham).WithMany(p => p.ChiTietSuaChuas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSu__SanPh__6EF57B66");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.DanhMucId).HasName("PK__DanhMuc__1C53BA7B3EE29315");
            entity.Property(e => e.DanhMucId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<LinhKien>(entity =>
        {
            entity.HasKey(e => e.LinhKienId).HasName("PK__LinhKien__04269C40A0329C9C"); }); }}