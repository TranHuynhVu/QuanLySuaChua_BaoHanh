using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("SanPham")]
public partial class SanPham
{
    [Key]
    [Column("SanPhamID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã sản phẩm")]
    public string SanPhamId { get; set; }

    [Column("KhachHangID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã khách hàng")]
    public string KhachHangId { get; set; }

    [Column("DanhMucID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã danh mục")]
    public string DanhMucId { get; set; }

    [Column("MaBH")]
    [StringLength(100)]
    [Unicode(false)]
    [Display(Name = "Mã bảo hành")]
    public string? MaBh { get; set; }

    [StringLength(255)]
    [Display(Name = "Tên sản phẩm")]
    public string TenSanPham { get; set; } = null!;

    [Display(Name = "Ngày mua")]
    public DateOnly NgayMua { get; set; }

    [Display(Name = "Ngày bảo hành")]
    public int ThoiGianBaoHanh { get; set; }

    public string? UrlHinhAnh { get; set; }

    [Column("NgayHetHanBH")]
    [Display(Name = "Ngày hết hạn bảo hành")]
    public DateOnly NgayHetHanBh { get; set; }

    [StringLength(1000)]
    [Display(Name = "Mô tả")]
    public string? MoTa { get; set; }

    [InverseProperty("SanPham")]
    public virtual ICollection<ChiTietSuaChua> ChiTietSuaChuas { get; set; } = new List<ChiTietSuaChua>();

    [ForeignKey("DanhMucId")]
    [InverseProperty("SanPhams")]
    [Display(Name = "Danh mục")]
    public virtual DanhMuc? DanhMuc { get; set; }

    [ForeignKey("KhachHangId")]
    [InverseProperty("SanPhams")]
    [Display(Name = "Khách hàng")]
    public virtual NguoiDung? KhachHang { get; set; }
}