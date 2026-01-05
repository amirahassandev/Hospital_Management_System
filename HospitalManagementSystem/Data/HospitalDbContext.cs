using System;
using System.Collections.Generic;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<NurseDoctor> NurseDoctors { get; set; }

    public virtual DbSet<NursePatient> NursePatients { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomStatus> RoomStatuses { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:hospitalConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.AdmissionsId).HasName("PK__Admissio__DF73CF6C56EDB1D7");

            entity.Property(e => e.AdmissionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADMISSIONS_PATIENT");

            entity.HasOne(d => d.Room).WithMany(p => p.Admissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADMISSIONS_ROOM");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentsId).HasName("PK__Appointm__970C424B7CDD5524");

            entity.Property(e => e.AppointmentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENTS_APPOINTMENT");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENTS_Doctor");

            entity.HasOne(d => d.Medical).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENTS_MEDICAL");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENTS_PATIENT");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.AppointmentStatusId).HasName("PK__Appointm__A619B660791453B5");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__Billing__F1656DF35B13CC3A");

            entity.Property(e => e.BillDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Billings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILLING_PATIENT");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Billings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILLING_STATUS");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED47C01AE0");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBF306437DE");

            entity.Property(e => e.YearsOfExperience).HasDefaultValue(0);

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SPECIALIZATION_USER");

            entity.HasOne(d => d.User).WithOne(p => p.Doctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCTOR_USER");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId).HasName("PK__Emergenc__E8A61D8E07B9ECA6");

            entity.HasOne(d => d.Patient).WithMany(p => p.EmergencyContacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMERGENCY_PATIENT");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalId).HasName("PK__MedicalR__4DD4333D1019E90B");

            entity.Property(e => e.RecordDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MEDICAL_DOCTOR");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MEDICAL_PATIENT");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9C98A49BBE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTACTMESSAGE_PATIENT");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.NurseId).HasName("PK__Nurse__4384784976214B9B");

            entity.HasOne(d => d.Department).WithMany(p => p.Nurses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSE_DEPARTMENT");

            entity.HasOne(d => d.User).WithOne(p => p.Nurse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSE_USER");
        });

        modelBuilder.Entity<NurseDoctor>(entity =>
        {
            entity.HasKey(e => e.NurseDoctorId).HasName("PK__NurseDoc__672458B22BB81985");

            entity.Property(e => e.NurseDoctorShift).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.NurseDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSEDOCTOR_Doctor");

            entity.HasOne(d => d.Nurse).WithMany(p => p.NurseDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSEDOCTOR_NURSE");
        });

        modelBuilder.Entity<NursePatient>(entity =>
        {
            entity.HasKey(e => e.NursePatientId).HasName("PK__NursePat__171421D9C80E6AE4");

            entity.Property(e => e.NursePatientShift).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Nurse).WithMany(p => p.NursePatients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSEPATIENT_NURSE");

            entity.HasOne(d => d.Patient).WithMany(p => p.NursePatients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NURSEPATIENT_PATIENT");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC36667B8D59B");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PATIENT_USER");
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId).HasName("PK__PaymentS__34F8AC3F125E0EC1");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionsId).HasName("PK__Prescrip__A2F3106937D7C84D");

            entity.HasOne(d => d.Medical).WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRESCRIPTIONS_MEDICAL");
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => e.ReceptionistId).HasName("PK__Receptio__0F8C20A82A95D711");

            entity.HasOne(d => d.User).WithOne(p => p.Receptionist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RECEPTIONIST_USER");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AC8766D4A");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__328639392178F7BA");

            entity.HasOne(d => d.Department).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROOM_DEPARTMENT");

            entity.HasOne(d => d.RoomStatus).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AROOM_STATUS");
        });

        modelBuilder.Entity<RoomStatus>(entity =>
        {
            entity.HasKey(e => e.RoomStatusId).HasName("PK__RoomStat__D29DF5167021A935");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D86FBCEA5884");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C536F00DC");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
