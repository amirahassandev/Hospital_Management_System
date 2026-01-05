using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("Nurse")]
[Index("UserId", Name = "UQ__Nurse__1788CC4D18FE76E4", IsUnique = true)]
public partial class Nurse
{
    [Key]
    public int NurseId { get; set; }

    public int UserId { get; set; }

    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Nurses")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Nurse")]
    public virtual ICollection<NurseDoctor> NurseDoctors { get; set; } = new List<NurseDoctor>();

    [InverseProperty("Nurse")]
    public virtual ICollection<NursePatient> NursePatients { get; set; } = new List<NursePatient>();

    [ForeignKey("UserId")]
    [InverseProperty("Nurse")]
    public virtual User User { get; set; } = null!;
}
