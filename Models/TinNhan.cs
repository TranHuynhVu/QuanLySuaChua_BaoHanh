using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("TinNhan")]
public partial class TinNhan
{
    [Key]
    [Column("TinNhanID")]
    [StringLength(20)]
    [Unicode(false)]
    public string TinNhanId { get; set; }

    [Column("NguoiGuiID")]
    [StringLength(20)]
    [Unicode(false)]
    public string NguoiGuiId { get; set; }

    [Column("NguoiNhanID")]
    [StringLength(20)]
    [Unicode(false)]
    public string NguoiNhanId { get; set; }

    [StringLength(2000)]
    public string? NoiDung { get; set; }

    [Precision(0)]
    public DateTime ThoiGianGui { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TrangThai { get; set; }

    [ForeignKey("NguoiGuiId")]
    [InverseProperty("TinNhanNguoiGuis")]
    public virtual NguoiDung NguoiGui { get; set; } = null!;

    [ForeignKey("NguoiNhanId")]
    [InverseProperty("TinNhanNguoiNhans")]
    public virtual NguoiDung NguoiNhan { get; set; } = null!;
}