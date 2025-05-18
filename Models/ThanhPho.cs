using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Models;

[Table("ThanhPho")]
public partial class ThanhPho
{
    [Key]
    [Column("ThanhPhoID")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Mã thành phố")]
    public string ThanhPhoId { get; set; }

    [StringLength(100)]
    [Display(Name = "Tên thành phố")]
    public string TenThanhPho { get; set; } = null!;

    [InverseProperty("ThanhPho")]
    public virtual ICollection<Quan> Quans { get; set; } = new List<Quan>();
}