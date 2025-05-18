using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("PhieuSuaChua")]
public partial class PhieuSuaChua
{
    [Key]
    [Column("PhieuSuaChuaID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name="Mã phiếu")]
    public string PhieuSuaChuaId { get; set; }

    [Column("KhachHangID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name="Mã khách hàng")]
    public string KhachHangId { get; set; }

    [Column("KyThuatID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name="Mã kĩ thuật viên")]
    public string? KyThuatId { get; set; }

    [Column("PhuongID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name="Mã phường")]
    public string PhuongId { get; set; }

    [StringLength(500)]
    [Display(Name="Mô tả của khách hàng")]
    public string MoTaKhachHang { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
  
    [Display(Name="Trạng thái")]
    public string? TrangThai { get; set; }

    [Precision(0)]
    [Display(Name="Ngày gửi")]
    public DateTime NgayGui { get; set; }

    [Precision(0)]
    [Display(Name="Ngày hẹn")]
    public DateTime? NgayHen { get; set; }

    [Precision(0)]
    [Display(Name="Ngày trả")]
    public DateTime? NgayTra { get; set; }

    [StringLength(500)]
    [Display(Name="Địa chỉ sản phẩm")]
    public string DiaChiNhanTraSanPham { get; set; } = null!;

    [Display(Name="Khoảng cách")]
    public double? KhoangCach { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    [Display(Name="Phí vận chuyển")]
    public decimal? PhiVanChuyen { get; set; }

    [Precision(0)]
    [Display(Name="Ngày thanh toán")]
    public DateTime? NgayThanhToan { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    [Display(Name="Phương thức thanh toán")]
    public string? PhuongThucThanhToan { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    [Display(Name="Tổng tiền")]
    public decimal? TongTien { get; set; }

    [StringLength(500)]
    [Display(Name="Ghi chú")]
    public string? GhiChu { get; set; }

    [InverseProperty("PhieuSuaChua")]
    public virtual ICollection<ChiTietPx> ChiTietPxes { get; set; } = new List<ChiTietPx>();

    [InverseProperty("PhieuSuaChua")]
    public virtual ICollection<ChiTietSuaChua> ChiTietSuaChuas { get; set; } = new List<ChiTietSuaChua>();

    [ForeignKey("KhachHangId")]
    [InverseProperty("PhieuSuaChuaKhachHangs")]
    [Display(Name="Mã Khách hàng")]
    public virtual NguoiDung? KhachHang { get; set; }

    [ForeignKey("KyThuatId")]
    [InverseProperty("PhieuSuaChuaKyThuats")]
    [Display(Name="Mã kỹ thuật viên")]
    public virtual NguoiDung? KyThuat { get; set; }

    [ForeignKey("PhuongId")]
    [InverseProperty("PhieuSuaChuas")]
    public virtual Phuong? Phuong { get; set; }
}
