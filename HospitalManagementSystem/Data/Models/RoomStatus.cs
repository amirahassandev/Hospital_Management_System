using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("RoomStatus")]
public partial class RoomStatus
{
    [Key]
    public int RoomStatusId { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [InverseProperty("RoomStatus")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
