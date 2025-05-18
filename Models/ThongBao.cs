using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("ThongBao")]
public partial class ThongBao
{
    [Key]
    [Column("ThongBaoID")]
    [StringLength(20)]
    [Unicode(false)]
    public string ThongBaoId { get; set; }

    [Column("NguoiDungID")]
    [StringLength(20)]
    [Unicode(false)]
    public string NguoiDungId { get; set; }

    [StringLength(255)]
    public string TieuDe { get; set; } = null!;

    [StringLength(1000)]
    public string? NoiDung { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TrangThai { get; set; }

    [Precision(0)]
    public DateTime NgayTao { get; set; }

    [ForeignKey("NguoiDungId")]
    [InverseProperty("ThongBaos")]
    public virtual NguoiDung NguoiDung { get; set; } = null!;
}