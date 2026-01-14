using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

public partial class Prescription
{
    [Key]
    public int PrescriptionsId { get; set; }

    [StringLength(50)]
    public string MedicationName { get; set; } = null!;

    [StringLength(50)]
    public string Dosage { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int MedicalId { get; set; }

    [ForeignKey("MedicalId")]
    [InverseProperty("Prescriptions")]
    public virtual MedicalRecord Medical { get; set; } = null!;
}
