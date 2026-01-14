using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("Department")]
public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [StringLength(50)]
    public string DepartmentName { get; set; } = null!;

    [StringLength(200)]
    public string? DepartmentDescription { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();

    [InverseProperty("Department")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
