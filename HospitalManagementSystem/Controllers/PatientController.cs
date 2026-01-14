using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Dto.User;
using HospitalManagementSystem.Repositories;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientsController(IPatientService service)
        {
            this._service = service;
        }

        // ################  GET   ################ //
        // Get: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAll();
            if (!patients.Any())
            {
                return NoContent(); // 204
            }
            return Ok(patients);
        }

        // Get: api/patients/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _service.GetPatient(id);
            if (patient == null)
            {
                return NotFound("The Patient Not Found");
            }
            return Ok(patient);
        }

        [HttpGet("deactivetedPatients")]
        public async Task<IActionResult> GetAllDeactivated()
        {
            var patients = await _service.GetAllDeactivatedPatientsAsync();
            if (!patients.Any())
            {
                return NoContent();
            }
            return Ok(patients);
        }

        [HttpGet("{id}/getBloodType")]
        public async Task<IActionResult> GetBloodType(int id)
        {
            var patient = await _service.GetBloodTypeAsync(id);
            if (patient == null)
            {
                return NotFound("The Patient Not Found");
            }
            return Ok(patient);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            int count = await _service.CountAsync();
            if (count == 0)
            {
                return NoContent();
            }
            return Ok(count);
        }


        // ################  POST   ################ /
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientDto patientDto)
        {
            var result = await _service.AddPatientAsync(patientDto);

            if (result == null)
                return BadRequest("Patient could not be created");

            return Ok(result);
        }

        // ################  UPDATE   ################ /
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto userDto)
        {
            if (userDto == null) return BadRequest("The Patient is null");
            var user = await _service.UpdatePatientAsync(id, userDto);
            if (user == null) return BadRequest("The Patient not exist");
            return Ok(user);
        }

        [HttpPut("{id}/reactivate")]
        public async Task<IActionResult> ReactivatePatient(int id)
        {
            var patient = await _service.GetPatient(id);
            if (patient == null) return NotFound(new { message = "This patient does not exist" });
            if (patient.IsActive)
            {
                return Ok(new { message = "This patient is already active", patient });
            }
            
            patient = await _service.ReactivatePatientAsync(id);
            return Ok(new { message = "Patient reactivated", patient });
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivatePatient(int id)
        {
            var patient = await _service.GetPatient(id);
            if (patient == null) return NotFound(new { message = "This patient does not exist" });
            if (!patient.IsActive)
            {
                return Ok(new { message = "This patient is already inactive", patient });
            }

            patient = await _service.DeactivatePatientAsync(id);
            return Ok(new { message = "Patient Deactivated", patient });
        }

        // ################  DELETE   ################ /
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _service.GetPatient(id);
            if (patient == null) return NotFound(new { message = "This patient does not exist" });
            if (!patient.IsActive)
            {
                return Ok(new { message = "This patient is already inactive", patient });
            }

            patient = await _service.DeletePatientAsync(id);
            return Ok(new { message = "Patient Deactivated", patient });
        }

        // ################  IS ??   ################ /
        [HttpGet("{id}/IsActive")]
        public async Task<IActionResult> IsActive(int id)
        {
            bool isActivated = await _service.IsActiveAsync(id);
            if (isActivated)
            {
                return Ok(new { message = "The user is active"});
            }
            return Ok(new { message = "The user is inactive"});
        }

        [HttpGet("{id}/IsAdult")]
        public async Task<IActionResult> IsAdult(int id)
        {
            bool isAdult = await _service.IsAdultAsync(id);
            return Ok(new
            {
                isAdult,
                message = isAdult ? "The user is an adult." : "The user is not an adult."
            });
        }


        // ################  SEARCH   ################ //
        [HttpGet("searchByAge")]
        public async Task<IActionResult> SearchByAge([FromQuery] int minAge, [FromQuery] int maxAge)
        {
            var patients = await _service.SearchByAgeAsync(minAge, maxAge);
            if (patients == null) return NoContent();
            return Ok(patients);
        }

        [HttpGet("searchByBloodType")]
        public async Task<IActionResult> SearchByBloodTypeAsync([FromQuery] string bloodType)
        {
            var patients = await _service.SearchByBloodTypeAsync(bloodType);
            if (patients == null) return NoContent();
            return Ok(patients);
        }

        [HttpGet("searchByGender")]
        public async Task<IActionResult> SearchByGenderAsync([FromQuery] string gender)
        {
            var patients = await _service.SearchByGenderAsync(gender);
            return Ok(patients);
        }

        [HttpGet("searchByName")]
        public async Task<IActionResult> SearchByNameAsync([FromQuery] string name)
        {
            var patients = await _service.SearchByNameAsync(name);
            return Ok(patients);
        }
    }
}
