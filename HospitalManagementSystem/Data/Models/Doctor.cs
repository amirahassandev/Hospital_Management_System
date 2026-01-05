using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("Doctor")]
[Index("UserId", Name = "UQ__Doctor__1788CC4DE3414E4C", IsUnique = true)]
public partial class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    public int? YearsOfExperience { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Certificate { get; set; }

    public int SpecializationId { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Doctor")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("Doctor")]
    public virtual ICollection<NurseDoctor> NurseDoctors { get; set; } = new List<NurseDoctor>();

    [ForeignKey("SpecializationId")]
    [InverseProperty("Doctors")]
    public virtual Specialization Specialization { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Doctor")]
    public virtual User User { get; set; } = null!;
}
