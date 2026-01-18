
namespace HospitalManagementSystem.Dto.Billing
{
    public class ReadBillingDto
    {
        public int BillingId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateOnly? BillDate { get; set; }
        public int PaymentStatusId { get; set; } = 2;
        public string? PaymentStatus { get; set; }
        public int PatientId { get; set; }
        public string? patientName { get; set; }
    }
}
