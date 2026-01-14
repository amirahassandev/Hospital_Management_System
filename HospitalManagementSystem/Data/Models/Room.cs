using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("Room")]
public partial class Room
{
    [Key]
    public int RoomId { get; set; }

    [StringLength(20)]
    public string RoomNumber { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int RoomStatusId { get; set; }

    [InverseProperty("Room")]
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Rooms")]
    public virtual Department Department { get; set; } = null!;

    [ForeignKey("RoomStatusId")]
    [InverseProperty("Rooms")]
    public virtual RoomStatus RoomStatus { get; set; } = null!;
}
