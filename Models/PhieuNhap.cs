using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("PhieuNhap")]
public partial class PhieuNhap
{
    [Key]
    [Column("PhieuNhapID")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhieuNhapId { get; set; }

    [Column("KhoID")]
    [StringLength(20)]
    [Unicode(false)]
    public string KhoId { get; set; }

    [Precision(0)]
    public DateTime NgayNhap { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal? TongTien { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TrangThai { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    [InverseProperty("PhieuNhap")]
    public virtual ICollection<ChiTietPn> ChiTietPns { get; set; } = new List<ChiTietPn>();

    [ForeignKey("KhoId")]
    [InverseProperty("PhieuNhaps")]
    public virtual NguoiDung Kho { get; set; } = null!;
}