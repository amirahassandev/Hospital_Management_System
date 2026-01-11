using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.MediaclRecord;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaclRecordController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public MediaclRecordController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        /* Get All MedicalRecords */
        // GET: api/MedicalRecord/getAll
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<MedicalRecordReadDto>>> GetAllMedicalRecords()
        {
            var medicalRecords= await _db.MedicalRecords
                .Include(mr => mr.Patient).ThenInclude(m => m.User)
                .Include(mr => mr.Doctor).ThenInclude(m => m.User)
                .Include(m => m.Prescriptions)
                .Include(m => m.Appointments)
                .AsNoTracking()
                 .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MedicalRecordReadDto>>(medicalRecords));
        }
        /* GetById*/
        // Get: api/MedicalRecord/2
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordReadDto>> GetById(int id)
        {
            var medicalRecord = await _db.MedicalRecords
                .Include(mr => mr.Patient).ThenInclude(m => m.User)
                .Include(mr => mr.Doctor).ThenInclude(m => m.User)
                .Include(m => m.Prescriptions)
                .Include(m => m.Appointments)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MedicalId == id);
            if (medicalRecord == null) return NotFound();

            return Ok(_mapper.Map<MedicalRecordReadDto>(medicalRecord));
        }
        /* delete mediacl record*/
        // Delete: api/MedicalRecord/2

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _db.MedicalRecords
                .Include(r => r.Prescriptions)
                .Include(r => r.Appointments)
                .FirstOrDefaultAsync(r => r.MedicalId == id);

            if (record == null)
                return NotFound();

            if (record.Prescriptions.Any() || record.Appointments.Any())
                return BadRequest("Cannot delete medical record with prescriptions or appointments.");

            _db.MedicalRecords.Remove(record);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Medical record deleted successfully." });
        }
        /* Update medical record*/
        // Put: api/MedicalRecord/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] MedicalRecordUpdateDto dto)
        {
            var record = await _db.MedicalRecords.FindAsync(id);
            if (record == null) return NotFound();
            _mapper.Map(dto, record);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Medical record updated successfully." });
        }
        /* create Mediacl Record*/
        // Post: api/MedicalRecord/create
        [HttpPost("create")]
        public async Task<ActionResult<MedicalRecordReadDto>> Create([FromBody] MedicalRecordCreateDto dto)
        {
            var patientExists = await _db.Patients.AnyAsync(p => p.PatientId == dto.PatientId);
            if (!patientExists)
                return BadRequest("Patient does not exist.");

            var doctorExists = await _db.Doctors.AnyAsync(d => d.DoctorId == dto.DoctorId);
            if (!doctorExists)
                return BadRequest("Doctor does not exist.");

            var record = _mapper.Map<MedicalRecord>(dto);
            _db.MedicalRecords.Add(record);
            await _db.SaveChangesAsync();

            await _db.Entry(record).Reference(r => r.Patient).Query().Include(p => p.User).LoadAsync();
            await _db.Entry(record).Reference(r => r.Doctor).Query().Include(d => d.User).LoadAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = record.MedicalId },
                _mapper.Map<MedicalRecordReadDto>(record));
        }

        }
}
