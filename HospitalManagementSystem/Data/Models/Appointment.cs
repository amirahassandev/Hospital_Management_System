using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

public partial class Appointment
{
    [Key]
    public int AppointmentsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AppointmentDate { get; set; }

    public int AppointmentStatusId { get; set; }

    public int MedicalId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    [ForeignKey("AppointmentStatusId")]
    [InverseProperty("Appointments")]
    public virtual AppointmentStatus AppointmentStatus { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("Appointments")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("MedicalId")]
    [InverseProperty("Appointments")]
    public virtual MedicalRecord Medical { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Appointments")]
    public virtual Patient Patient { get; set; } = null!;
}
