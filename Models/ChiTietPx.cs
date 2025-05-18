using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[PrimaryKey("PhieuXuatId", "PhieuSuaChuaId")]
[Table("ChiTietPX")]
public partial class ChiTietPx
{
    [Key]
    [Column("PhieuXuatID")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhieuXuatId { get; set; }

    [Key]
    [Column("PhieuSuaChuaID")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhieuSuaChuaId { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    [ForeignKey("PhieuSuaChuaId")]
    [InverseProperty("ChiTietPxes")]
    public virtual PhieuSuaChua PhieuSuaChua { get; set; } = null!;

    [ForeignKey("PhieuXuatId")]
    [InverseProperty("ChiTietPxes")]
    public virtual PhieuXuat PhieuXuat { get; set; } = null!;
}