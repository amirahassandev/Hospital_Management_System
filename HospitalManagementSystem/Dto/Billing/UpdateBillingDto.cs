namespace HospitalManagementSystem.Dto.Billing
{
    public class UpdateBillingDto
    {
        public decimal TotalAmount { get; set; } = 0;
        public int PaymentStatusId { get; set; } = 2;
    }
}
