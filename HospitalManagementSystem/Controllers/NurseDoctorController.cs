using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.NurseDoctor;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NurseDoctorController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public NurseDoctorController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }

        /* Get All NurseDoctors */
        // GET: api/NurseDoctor/getAllNurseDoctors
        [HttpGet("getAllNurseDoctors")]
        public async Task<ActionResult<IEnumerable<NurseDoctorReadDto>>> GetAll()
        {
            var nurseDoctors = await _db.NurseDoctors
                .Include(nd => nd.Nurse)
                .Include(nd => nd.Doctor)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<NurseDoctorReadDto>>(nurseDoctors));
        }

        /* Get By Id */
        // GET: api/NurseDoctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NurseDoctorReadDto>> GetById(int id)
        {
            var nurseDoctor = await _db.NurseDoctors
                .Include(nd => nd.Nurse)
                .Include(nd => nd.Doctor)
                .FirstOrDefaultAsync(nd => nd.NurseDoctorId == id);
            if (nurseDoctor == null)
            {
                return NotFound(new { Message = "NurseDoctor Not Found :(" });
            }
            return Ok(_mapper.Map<NurseDoctorReadDto>(nurseDoctor));
        }
        /* create */
        // POST: api/NurseDoctor/createNurseDoctor
        [HttpPost("createNurseDoctor")]
        public async Task<ActionResult<NurseDoctorReadDto>> CreateNurseDoctor([FromBody] NurseDoctorCreateDto nurseDoctorDto)
        {
            var nurseExists = await _db.Nurses.AnyAsync(n => n.NurseId == nurseDoctorDto.NurseId);
            if (!nurseExists)
                return BadRequest("Nurse does not exist.");
            var doctorExists = await _db.Doctors.AnyAsync(d => d.DoctorId == nurseDoctorDto.DoctorId);
            if (!doctorExists)
                return BadRequest("Doctor does not exist.");


            var nurseDoctor = _mapper.Map<NurseDoctor>(nurseDoctorDto);
            _db.NurseDoctors.Add(nurseDoctor);
            await _db.SaveChangesAsync();
            var createdNurseDoctor = await _db.NurseDoctors
                .Include(nd => nd.Nurse)
                .Include(nd => nd.Doctor)
                .FirstOrDefaultAsync(nd => nd.NurseDoctorId == nurseDoctor.NurseDoctorId);
            return CreatedAtAction(nameof(GetById), new { id = nurseDoctor.NurseDoctorId }, _mapper.Map<NurseDoctorReadDto>(createdNurseDoctor));
        }
        /* update */
        // PUT: api/NurseDoctor/5
        [HttpPut("{id}")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNurseDoctor(
    int id,
    [FromBody] NurseDoctorUpdateDto dto)
        {
            var nurseDoctor = await _db.NurseDoctors.FindAsync(id);
            if (nurseDoctor == null)
            {
                return NotFound(new { message = "NurseDoctor not found." });
            }

            _mapper.Map(dto, nurseDoctor);
            await _db.SaveChangesAsync();

            return Ok(new { message = "created successfully." });
        }

        /* delete */
        // DELETE: api/NurseDoctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurseDoctor(int id)
        {
            var nurseDoctor = await _db.NurseDoctors.FindAsync(id);
            if (nurseDoctor == null)
            {
                return NotFound(new { Message = "NurseDoctor Not Found :(" });
            }
            _db.NurseDoctors.Remove(nurseDoctor);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Deleted successfully." });
        }
    }
}
