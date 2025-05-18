using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("Quan")]
public partial class Quan
{
    [Key]
    [Column("QuanID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã quận")]
    public string QuanId { get; set; }

    [StringLength(100)]
    [Display(Name = "Tên quận")]
    public string TenQuan { get; set; } = null!;

    [Column("ThanhPhoID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã thành phố")]
    public string ThanhPhoId { get; set; }

    [InverseProperty("Quan")]
    public virtual ICollection<Phuong> Phuongs { get; set; } = new List<Phuong>();

    [ForeignKey("ThanhPhoId")]
    [InverseProperty("Quans")]
    public virtual ThanhPho? ThanhPho { get; set; }
}