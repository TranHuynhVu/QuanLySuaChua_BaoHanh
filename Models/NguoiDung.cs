using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("NguoiDung")]
[Index("UserName", Name = "UQ__NguoiDun__D5B8C7F043064735", IsUnique = true)]
public partial class NguoiDung : IdentityUser<string>
{
    [Key]
    [Column("NguoiDungID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã người dùng")]
    public override string Id { get; set; }

    [Required, StringLength(50), Unicode(false)]
    [Column("TaiKhoan")]
    [Display(Name = "Tên tài khoản")]
    public override string UserName { get; set; } = null!;

    [Required, StringLength(255), Unicode(false)]
    [Column("MatKhau")]
    [Display(Name = "Mật khẩu")]
    public override string PasswordHash { get; set; } = null!;

    [StringLength(150)]
    [Column("HoTen")]
    [Display(Name = "Họ tên")]
    public string? HoTen { get; set; }

    [StringLength(255), Unicode(false)]
    [Column("Email")]
    [Display(Name = "Email")]
    public override string? Email { get; set; }

    [StringLength(20), Unicode(false)]
    [Column("Sdt")]
    [Display(Name = "Số điện thoại")]
    public override string? PhoneNumber { get; set; }

    [Column("PhuongID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã phường")]
    public string PhuongId { get; set; }

    [StringLength(20), Unicode(false)]
    [Column("VaiTro")]
    [Display(Name = "Vai trò")]
    public string? VaiTro { get; set; }

    [StringLength(500)]
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    [InverseProperty("Kho")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    [InverseProperty("KhachHang")]
    public virtual ICollection<PhieuSuaChua> PhieuSuaChuaKhachHangs { get; set; } = new List<PhieuSuaChua>();

    [InverseProperty("KyThuat")]
    public virtual ICollection<PhieuSuaChua> PhieuSuaChuaKyThuats { get; set; } = new List<PhieuSuaChua>();

    [InverseProperty("Kho")]
    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();

    [ForeignKey("PhuongId")]
    [InverseProperty("NguoiDungs")]
    public virtual Phuong Phuong { get; set; } = null!;

    [InverseProperty("KhachHang")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();

    [InverseProperty("NguoiDung")]
    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();

    [InverseProperty("NguoiGui")]
    public virtual ICollection<TinNhan> TinNhanNguoiGuis { get; set; } = new List<TinNhan>();

    [InverseProperty("NguoiNhan")]
    public virtual ICollection<TinNhan> TinNhanNguoiNhans { get; set; } = new List<TinNhan>();

}