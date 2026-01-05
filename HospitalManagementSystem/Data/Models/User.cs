using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Models;

[Table("User")]
[Index("Email", Name = "UQ__User__A9D10534C6DA49C1", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool Gender { get; set; }

    public int? Age { get; set; }

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    public int RoleId { get; set; }

    [InverseProperty("User")]
    public virtual Doctor? Doctor { get; set; }

    [InverseProperty("User")]
    public virtual Nurse? Nurse { get; set; }

    [InverseProperty("User")]
    public virtual Patient? Patient { get; set; }

    [InverseProperty("User")]
    public virtual Receptionist? Receptionist { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
