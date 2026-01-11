using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.Doctor;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public DoctorsController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        /*
         * Get All Doctors
          */

        // GET: api/Doctors/getAllDoctors
        [HttpGet("getAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAll()
        {
            var doctors = await _db.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DoctorReadDto>>(doctors));
        }

        /* Get By Id */
        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadDto>> GetById(int id)
        {
            var doctor = await _db.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound(new { Message = "Doctor Not Found :(" });
            }
            return Ok(_mapper.Map<DoctorReadDto>(doctor));
        }
        /* Create Doctor */
        // POST: api/Doctors/createDoctor
        [HttpPost("createDoctor")]
        public async Task<ActionResult<DoctorReadDto>> CreateDoctor([FromBody] DoctorCreateDto doctorDto)
        {
            var userExists = await _db.Users.AnyAsync(u => u.UserId == doctorDto.UserId);
            if (!userExists)
                return BadRequest("User does not exist.");

            var specializationExists = await _db.Specializations
                .AnyAsync(s => s.SpecializationId == doctorDto.SpecializationId);
            if (!specializationExists)
                return BadRequest("Specialization does not exist.");

            var doctor = _mapper.Map<Doctor>(_db);
            doctor.IsActive = true;

            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();

            // reload with navigation properties
            await _db.Entry(doctor).Reference(d => d.User).LoadAsync();
            await _db.Entry(doctor).Reference(d => d.Specialization).LoadAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = doctor.DoctorId },
                _mapper.Map<DoctorReadDto>(doctor));

        }
        /* Update Doctor */
        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctor( int id , [FromBody] DoctorUpdateDto doctorDto)
        {
            var doctor = await _db.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound(new { Message = "Doctor Not Found :(" });
            }
            _mapper.Map(doctorDto, doctor);
            _db.Doctors.Update(doctor);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Doctor created successfully." });
        }
        /* Delete Doctor */
        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var doctor = await _db.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound(new { Message = "Doctor Not Found :(" });
            }
            _db.Doctors.Remove(doctor);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Doctor deleted successfully." });
        }
    }
}
