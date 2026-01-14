using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("EmergencyContact")]
public partial class EmergencyContact
{
    [Key]
    public int EmergencyContactId { get; set; }

    [StringLength(20)]
    public string Contact { get; set; } = null!;

    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("EmergencyContacts")]
    public virtual Patient Patient { get; set; } = null!;
}
