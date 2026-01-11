using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.Appointment;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public AppointmentController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        // GET: api/appointments/getall
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<AppointmentReadDto>>> GetAll()
        {
            var appointments = await _db.Appointments
                .Include(a => a.AppointmentStatus)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Medical)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<AppointmentReadDto>>(appointments));
        }
        // GET: api/appointments/2
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentReadDto>> GetById(int id)
        {
            var appointment = await _db.Appointments
                .Include(a => a.AppointmentStatus)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Medical)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AppointmentsId == id);
            if (appointment == null) return NotFound();
            return Ok(_mapper.Map<AppointmentReadDto>(appointment));
        }
        // POST: api/appointments/create
        [HttpPost("create")]
        public async Task<ActionResult<AppointmentReadDto>> CreateAppointment([FromBody] AppointmentCreateDto appointmentCreateDto)
        {
            if (!await _db.Doctors.AnyAsync(d => d.DoctorId == appointmentCreateDto.DoctorId))
            {
                return BadRequest("Doctor does not exist.");

            }
            if (!await _db.Patients.AnyAsync(d => d.PatientId == appointmentCreateDto.PatientId))
            {
                return BadRequest("Patient does not exist.");
            }
            if (!await _db.MedicalRecords.AnyAsync(d => d.MedicalId == appointmentCreateDto.MedicalId))
            {
                return BadRequest("Medical Record does not exist.");

            }
            if (!await _db.AppointmentStatuses.AnyAsync(s => s.AppointmentStatusId == appointmentCreateDto.AppointmentStatusId))
                return BadRequest("Appointment status does not exist.");

            var appoitment = _mapper.Map<Appointment>(appointmentCreateDto);
            _db.Appointments.Add(appoitment);
            await _db.SaveChangesAsync();


            await _db.Entry(appoitment).Reference(a => a.AppointmentStatus).LoadAsync();
            await _db.Entry(appoitment).Reference(a => a.Doctor).Query().Include(d => d.User).LoadAsync();
            await _db.Entry(appoitment).Reference(a => a.Patient).Query().Include(p => p.User).LoadAsync();
            await _db.Entry(appoitment).Reference(a => a.Medical).LoadAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = appoitment.AppointmentsId },
                _mapper.Map<AppointmentReadDto>(appoitment));
        }
        // PUT: api/appointments/3
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentUpdateDto dto)
        {
            var appointment = await _db.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            if (!await _db.AppointmentStatuses.AnyAsync(s => s.AppointmentStatusId == dto.AppointmentStatusId))
                return BadRequest("Appointment status does not exist.");

            _mapper.Map(dto, appointment);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Appointment created successfully." });
        }
        // Delete: api/appointments/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _db.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _db.Appointments.Remove(appointment);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Appointment deleted successfully." });
        }
    }
}
