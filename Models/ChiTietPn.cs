using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[PrimaryKey("PhieuNhapId", "LinhKienId")]
[Table("ChiTietPN")]
public partial class ChiTietPn
{
    [Key]
    [Column("PhieuNhapID")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhieuNhapId { get; set; }

    [Key]
    [Column("LinhKienID")]
    [StringLength(20)]
    [Unicode(false)]
    public string LinhKienId { get; set; }

    public int SoLuong { get; set; }

    [ForeignKey("LinhKienId")]
    [InverseProperty("ChiTietPns")]
    public virtual LinhKien LinhKien { get; set; } = null!;

    [ForeignKey("PhieuNhapId")]
    [InverseProperty("ChiTietPns")]
    public virtual PhieuNhap PhieuNhap { get; set; } = null!;
}