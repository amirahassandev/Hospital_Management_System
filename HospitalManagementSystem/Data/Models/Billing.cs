using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("Billing")]
public partial class Billing
{
    [Key]
    public int BillingId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    public DateOnly? BillDate { get; set; }

    public int PaymentStatusId { get; set; }

    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Billings")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("Billings")]
    public virtual PaymentStatus PaymentStatus { get; set; } = null!;
}
