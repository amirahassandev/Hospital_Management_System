using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Billing;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : Controller
    {
        private readonly IBillingService _service;

        public BillingController(IBillingService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBilling(AddBillingDto billingDto)
        {
            if (billingDto == null) return NotFound("Please, enter total amount, PaymentStatusId, and PatientId");
            bool isExisted = await _service.IsExist(billingDto.PatientId, billingDto.PaymentStatusId);
            if (isExisted) return NotFound("This billing already exist");
            var readBilling = await _service.CreateBillingAsync(billingDto);
            return Ok(new { Message = "Billing has been added successfully.", readBilling });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var billings = await _service.GetAllBillingsAsync();
            if (billings == null || !billings.Any()) return NotFound("Not Exist any bill");
            return Ok(billings);
        }

        [HttpGet("{billId}")]
        public async Task<IActionResult> GetBilling(int billId)
        {
            var bill = await _service.GetBillingAsync(billId);
            if (bill == null) return NotFound("Not Exist any bill");
            return Ok(bill);
        }

        [HttpPut("{billId}")]
        public async Task<IActionResult> UpdateBilling(int billId, UpdateBillingDto billingDto)
        {
            if (billingDto == null) return NotFound("you do not add Total Amount and id of Payment Status");
            var bill = await _service.GetBillingAsync(billId);
            if (bill == null) return NotFound("this bill not exist");
            if (bill.PaymentStatusId == 1) return BadRequest("This bill cannot be updated because it is already paid.");
            if (billingDto.TotalAmount != 0)
            {
                bill.TotalAmount = billingDto.TotalAmount;
            }
            if(billingDto.PaymentStatusId != 1)
            {
                bill.PaymentStatusId = billingDto.PaymentStatusId;
            }
            var readBill = await _service.UpdateBillingAsync(billId, billingDto);
            return Ok(new {Message = "The bill updated, successfully", readBill});
        }

        [HttpDelete("{billId}")]
        public async Task<IActionResult> DeleteBilling(int billId)
        {
            var bill = await _service.GetBillingAsync(billId);
            if (bill == null) return NotFound("This bill does not exist");
            if (bill.PaymentStatusId == 1) return BadRequest("This bill cannot be deleted because it is already paid.");
            bool isDeleted = await _service.DeleteBillingAsync(billId);
            if (!isDeleted) return NotFound("Deleted operation falid");
            return Ok(new { Message = "The bill deleted successfully", bill });
        }
    }
}
