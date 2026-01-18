using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NursePatientController : ControllerBase
    {
        private readonly INursePatientService _service;
        private readonly IPatientService _servicePatient;
        //private readonly  _serviceNurse;


        public NursePatientController(INursePatientService _service, IPatientService _servicePatient)
        {
            this._service = _service;
            this._servicePatient = _servicePatient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNursePatient()
        {
            var allNursePatient = await _service.GetAllNursePatients();
            if(allNursePatient == null) return NotFound();
            return Ok(allNursePatient);
        }


        [HttpPost]
        public async Task<IActionResult> AssignNurseToPatient(AddNursePatientDto nursePatientDto)
        {
            if(nursePatientDto == null) return BadRequest("Put Id of the Patient and the Nurse");

            var patientExisted = await _servicePatient.GetPatient(nursePatientDto.PatientId);
            if (patientExisted == null) return BadRequest("The Patient does not exist");

            //var nurseExisted = await _serviceNurse.GetNurse(nursePatientDto.NurseId);
            //if (nurseExisted == null) return BadRequest("The Nurse does not exist");

            bool isExisted = await _service.IsNursePatientExist(nursePatientDto.NurseId, nursePatientDto.PatientId);
            if (isExisted) return BadRequest("This nurse already assigned for the patient");
            return Ok(await _service.AssignNurseToPatient(nursePatientDto));
        }

        [HttpPut]
        public async Task<IActionResult?> UpdateNursePatient(UpdateNursePatientDto nursePatientDto)
        {
            if (nursePatientDto == null) return BadRequest("Put Id of the Patient and the Nurse");
            bool isExisted = await _service.IsNursePatientExist(nursePatientDto.NurseId, nursePatientDto.PatientId);
            if (!isExisted) return BadRequest("This nurse does not assigned for the patient");
            var updatedNursePatient = await _service.UpdateNursePatient(nursePatientDto);
            return Ok(updatedNursePatient);
        }

        [HttpDelete("{nurseId}/{patientId}")]
        public async Task<IActionResult> RemoveNursePatient(int nurseId, int patientId)
        {
            bool isExisted = await _service.IsNursePatientExist(nurseId, patientId);
            if (!isExisted) return NotFound();
            var readNursePatient = await _service.RemoveNursePatient(nurseId, patientId);
            return Ok(new { Message = "This nurse does not assigned for the patient", readNursePatient });
        }

        [HttpGet("{patientId}/assignments")]
        public async Task<IActionResult> GetAssignmentsForPatient(int patientId)
        {
            var patient = await _servicePatient.GetPatient(patientId);
            if (patient == null) return NotFound();
            var patientAssignments = await _service.GetAssignmentsForPatient(patientId);
            if (patientAssignments == null || !patientAssignments.Any()) return NotFound("No assignments found for this patient.");
            return Ok(patientAssignments);
        }

    }
}
