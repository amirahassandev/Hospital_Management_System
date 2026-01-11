namespace HospitalManagementSystem.Data.Dto.Appointment
{
    public class AppointmentReadDto
    {
        public int AppointmentsId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int AppointmentStatusId { get; set; }
        public string AppointmentStatusName { get; set; } = null!;

        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; } = null!;
        public string DoctorLastName { get; set; } = null!;

        public int PatientId { get; set; }
        public string PatientFirstName { get; set; } = null!;
        public string PatientLastName { get; set; } = null!;

        public int MedicalId { get; set; }
        public string Diagnosis { get; set; } = null!;
    }
}
