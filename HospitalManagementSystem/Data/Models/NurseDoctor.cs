using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("NurseDoctor")]
public partial class NurseDoctor
{
    [Key]
    public int NurseDoctorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NurseDoctorShift { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    public int NurseId { get; set; }

    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("NurseDoctors")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("NurseId")]
    [InverseProperty("NurseDoctors")]
    public virtual Nurse Nurse { get; set; } = null!;
}
