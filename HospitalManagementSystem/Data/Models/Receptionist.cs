using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("Receptionist")]
[Index("UserId", Name = "UQ__Receptio__1788CC4D7CAF1B61", IsUnique = true)]
public partial class Receptionist
{
    [Key]
    public int ReceptionistId { get; set; }

    public DateOnly ReceptionistShift { get; set; }
    public bool IsActive { get; set; } = true;

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Receptionist")]
    public virtual User User { get; set; } = null!;
}
