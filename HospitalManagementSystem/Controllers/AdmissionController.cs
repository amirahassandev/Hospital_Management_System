using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Services.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _service;
        private readonly IPatientService _patientService;
        //private readonly IDepartmentService _departmentService;


        public AdmissionController(IAdmissionService _service, IPatientService _patientService /*, IDepartmentService _departmentService */) 
        {
            this._service = _service;
            this._patientService = _patientService;
            //this._departmentService = _departmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAdmissions()
        {
            var admissions = await _service.GetAllAdmissionsAsync();
            if(!admissions.Any()) return NotFound("Not Found Any Admissions");
            return Ok(admissions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmission(AddAdmissionDto admissionDto)
        {
            if (admissionDto == null) return NotFound("This admission with null");

            var patient = await _patientService.GetPatient(admissionDto.PatientId);
            if (patient == null) return NotFound("This Patient does not exist");

            //var department = await _departmentService.GetDepartmentById(admissionDto.DepartmentId);
            //if (department == null) return NotFound("This department does not exist");
            var roomNumber = admissionDto.RoomCreateDto.RoomNumber;
            if (roomNumber == null) return NotFound("The Number of Room is null");
            bool isRoomExisted = await _service.IsRoomExistedAsync(roomNumber);
            if (!isRoomExisted) return NotFound("This room does not exist");
            bool isRoomAvailable = await _service.IsRoomAvailableAsync(roomNumber);
            if (!isRoomAvailable) return NotFound("This room does not available");

            var readAdmission = await _service.CreateAdmissionAsync(admissionDto);
            if (readAdmission == null) return NotFound("Error in creation this admission");
            return Ok(readAdmission);
        }

        [HttpGet("{admissionId}")]
        public async Task<IActionResult> GetAdmissionById(int admissionId)
        {
            var admission = await _service.GetAdmissionByIdAsync(admissionId);
            if (admission == null) return NotFound("This admission does not exist");
            return Ok(admission);
        }


        [HttpPut("{admissionId}")]
        public async Task<IActionResult> UpdateAdmission(int admissionId, [FromBody] UpdateAdmissionDto admissionDto)
        {
            if (admissionDto == null)
                return BadRequest("Admission data is required.");

            if (admissionDto.AdmissionDate.HasValue &&
                admissionDto.DischargeDate.HasValue &&
                admissionDto.DischargeDate < admissionDto.AdmissionDate)
            {
                return BadRequest("Discharge date cannot be before admission date.");
            }

            if (admissionDto.PatientId != 0)
            {
                var patient = await _patientService.GetPatient(admissionDto.PatientId);
                if (patient == null)
                    return BadRequest("Invalid PatientId.");
            }

            if (!string.IsNullOrWhiteSpace(admissionDto.RoomNumber))
            {
                bool roomExists = await _service.IsRoomExistedAsync(admissionDto.RoomNumber);
                if (!roomExists)
                    return BadRequest("Invalid RoomNumber.");

                //var room = await _serviceRoom.GetRoomByNumber(admissionDto.RoomNumber);
                //if(room.RoomStatus.RoomStatusId == 3) return BadRequest("The Room in Maintenance");
                //if (room.RoomStatus.RoomStatusId == 2) return BadRequest("The room is occupied");

                bool roomAvailable = await _service.IsRoomAvailableAsync(admissionDto.RoomNumber);
                if (!roomAvailable)
                    return BadRequest("Room is not available.");
            }

            if (admissionDto.RoomStatusId < 0)
                return BadRequest("Invalid RoomStatusId.");

            var updatedAdmission = await _service.UpdateAdmissionAsync(admissionId, admissionDto);

            if (updatedAdmission == null)
                return NotFound("Admission not found or failed to update.");

            return Ok(updatedAdmission);
        }




    }
}
