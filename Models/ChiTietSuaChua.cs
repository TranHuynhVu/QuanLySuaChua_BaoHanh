using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("ChiTietSuaChua")]
public partial class ChiTietSuaChua
{
    [Key]
    [Column("ChiTietID")]
    [StringLength(20)]
    [Unicode(false)]
    public string ChiTietId { get; set; }

    [Column("SanPhamID")]
    [StringLength(20)]
    [Unicode(false)]
    public string SanPhamId { get; set; }

    [Column("PhieuSuaChuaID")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhieuSuaChuaId { get; set; }

    [Column("LinhKienID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? LinhKienId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? LoaiDon { get; set; }

    public int SoLuongLinhKien { get; set; }

    [StringLength(500)]
    public string? DanhGiaKyThuat { get; set; }

    [ForeignKey("LinhKienId")]
    [InverseProperty("ChiTietSuaChuas")]
    public virtual LinhKien LinhKien { get; set; } = null!;

    [ForeignKey("PhieuSuaChuaId")]
    [InverseProperty("ChiTietSuaChuas")]
    public virtual PhieuSuaChua PhieuSuaChua { get; set; } = null!;

    [ForeignKey("SanPhamId")]
    [InverseProperty("ChiTietSuaChuas")]
    public virtual SanPham SanPham { get; set; } = null!;
}