using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("PaymentStatus")]
[Index("StatusName", Name = "UQ__PaymentS__05E7698AEE2D715A", IsUnique = true)]
public partial class PaymentStatus
{
    [Key]
    public int PaymentStatusId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
}
