using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("AppointmentStatus")]
public partial class AppointmentStatus
{
    [Key]
    public int AppointmentStatusId { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [InverseProperty("AppointmentStatus")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
