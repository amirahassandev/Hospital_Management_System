namespace HospitalManagementSystem.Dto.Appointment
{
    public class AppointmentCreateDto
    {
        public DateTime AppointmentDate { get; set; }

        public int AppointmentStatusId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int MedicalId { get; set; }
    }
}
