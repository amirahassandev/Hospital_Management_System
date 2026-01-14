using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

public partial class Message
{
    [Key]
    public int MessageId { get; set; }

    [StringLength(200)]
    public string MessageDescription { get; set; } = null!;

    public int PatientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Messages")]
    public virtual Patient Patient { get; set; } = null!;
}
