using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("LinhKien")]
public partial class LinhKien
{
    [Key]
    [Column("LinhKienID")]
    [StringLength(20)]
    [Unicode(false)]
    public string LinhKienId { get; set; }

    [Column("DanhMucID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? DanhMucId { get; set; }

    [StringLength(255)]
    public string TenLinhKien { get; set; } = null!;

    public int SoLuongTon { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal DonGia { get; set; }

    [StringLength(255)]
    public string? PhamViSuDung { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    [InverseProperty("LinhKien")]
    public virtual ICollection<ChiTietPn> ChiTietPns { get; set; } = new List<ChiTietPn>();

    [InverseProperty("LinhKien")]
    public virtual ICollection<ChiTietSuaChua> ChiTietSuaChuas { get; set; } = new List<ChiTietSuaChua>();

    [ForeignKey("DanhMucId")]
    [InverseProperty("LinhKiens")]
    public virtual DanhMuc? DanhMuc { get; set; }
}