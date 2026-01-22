using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Prescription;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService _service)
        {
            this._service = _service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPrescription()
        {
            var prescriptions = await _service.GetAllPrescriptionsAsync();
            if (prescriptions == null) return NoContent();
            return Ok(prescriptions);
        }

        [HttpGet("{prescriptionId}")]
        public async Task<IActionResult> GetPrescriptionById(int prescriptionId)
        {
            if (prescriptionId <= 0) return NotFound("This prescription does not exist");
            var presecription = await _service.GetPrescriptionByIdAsync(prescriptionId);
            if (presecription == null) return NotFound("This prescription does not exist");
            return Ok(presecription);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(AddPrescriptionDto PrescriptionDto)
        {
            if (PrescriptionDto == null) return NotFound("This prescription does not exist");

            var medicalId = PrescriptionDto.MedicalId;
            //bool isMedicalRecordExist = await _serviceMedical.IsExist(medicalId);
            //if (!isMedicalRecordExist) return NotFound("This medical record does not exist");

            var prescription = await _service.CreatePrescriptionAsync(PrescriptionDto);
            if(prescription == null) return NotFound("This prescription does not exist");
            return Ok(prescription);
        }

        [HttpPut("{prescriptionId}")]
        public async Task<IActionResult> UpdatePrescription(int prescriptionId, [FromBody] UpdatePrescriptionDto prescriptionDto)
        {
            var prescription = await _service.UpdatePrescriptionAsync(prescriptionId, prescriptionDto);

            var medicalId = prescription.medicalRecordDto.MedicalId;
            //bool isMedicalRecordExist = await _serviceMedical.IsExist(medicalId);
            //if(!isMedicalRecordExist) return NotFound("This medical record does not exist");

            if (prescription == null) return NotFound("This prescription does not exist");

            return Ok(prescription);
        }
    }
}
