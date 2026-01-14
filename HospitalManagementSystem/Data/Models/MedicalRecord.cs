using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

public partial class MedicalRecord
{
    [Key]
    public int MedicalId { get; set; }

    [StringLength(50)]
    public string Diagnosis { get; set; } = null!;

    [StringLength(50)]
    public string Treatment { get; set; } = null!;

    [StringLength(50)]
    public string Notes { get; set; } = null!;

    public DateOnly? RecordDate { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    [InverseProperty("Medical")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("DoctorId")]
    [InverseProperty("MedicalRecords")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("MedicalRecords")]
    public virtual Patient Patient { get; set; } = null!;

    [InverseProperty("Medical")]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
