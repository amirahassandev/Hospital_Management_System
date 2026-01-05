using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("NursePatient")]
public partial class NursePatient
{
    [Key]
    public int NursePatientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NursePatientShift { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    public int NurseId { get; set; }

    public int PatientId { get; set; }

    [ForeignKey("NurseId")]
    [InverseProperty("NursePatients")]
    public virtual Nurse Nurse { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("NursePatients")]
    public virtual Patient Patient { get; set; } = null!;
}
