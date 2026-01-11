using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("Patient")]
[Index("UserId", Name = "UQ__Patient__1788CC4DA0CE33C8", IsUnique = true)]
public partial class Patient
{
    [Key]
    public int PatientId { get; set; }

    [StringLength(20)]
    public string BloodType { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public int UserId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    [InverseProperty("Patient")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Patient")]
    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    [InverseProperty("Patient")]
    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    [InverseProperty("Patient")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("Patient")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [InverseProperty("Patient")]
    public virtual ICollection<NursePatient> NursePatients { get; set; } = new List<NursePatient>();

    [ForeignKey("UserId")]
    [InverseProperty("Patient")]
    public virtual User User { get; set; } = null!;
}
