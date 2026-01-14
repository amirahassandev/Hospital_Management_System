using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("Specialization")]
public partial class Specialization
{
    [Key]
    public int SpecializationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SpecializationName { get; set; } = null!;

    [InverseProperty("Specialization")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
