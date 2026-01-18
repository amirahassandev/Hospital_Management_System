using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Dto.Billing
{
    public class AddBillingDto
    {
        public decimal TotalAmount { get; set; }
        public int PaymentStatusId { get; set; } = 2;
        public int PatientId { get; set; }
    }
}
