using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

public partial class Admission
{
    [Key]
    public int AdmissionsId { get; set; }

    public DateOnly? AdmissionDate { get; set; }

    public DateOnly? DischargeDate { get; set; }

    public int RoomId { get; set; }

    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Admissions")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("Admissions")]
    public virtual Room Room { get; set; } = null!;
}
