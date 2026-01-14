using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Nurse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NurseController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public NurseController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        /* Get All Nurse*/
        // GET: api/Nurse/getAllNurses
        [HttpGet("getAllNurses")]
        public async Task<ActionResult<IEnumerable<NurseReadDto>>> GetAllNurse()
        {
            var nurses = _db.Nurses.Include(n => n.User).
                Include(n => n.Department)
                .AsNoTracking()
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<NurseReadDto>>(await nurses));

        }

        /* Get By Id */
        // GET: api/Nurse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NurseReadDto>>> GetNurseById( int id)
        {
            var nurse = _db.Nurses.Include(n => n.User)
                .Include(n => n.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(N => N.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<NurseReadDto>(nurse));
        }

        /* Create Nurse */
        // POST: api/Nurse/createNurse
        [HttpPost("createNurse")]
        public async Task<ActionResult<NurseReadDto>> CreateNurse([FromBody] NurseCreateDto dto)
        {
            var UserExit = await _db.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!UserExit)
            {
                return BadRequest("User Doesn't exist");
            }
            var departmentExists = await _db.Departments
                .AnyAsync(d => d.DepartmentId == dto.DepartmentId);
            if (!departmentExists)
                return BadRequest("Department does not exist.");

            var nurse = _mapper.Map<Nurse>(dto);

            _db.Nurses.Add(nurse);
            await _db.SaveChangesAsync();

            await _db.Entry(nurse).Reference(n => n.User).LoadAsync();
            await _db.Entry(nurse).Reference(n => n.Department).LoadAsync();

            return CreatedAtAction(nameof(GetNurseById),
                new { id = nurse.NurseId },
                _mapper.Map<NurseReadDto>(nurse));
        }

        /* Update Nurse */
        // Put: api/Nurse/2
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNurse(int id ,[FromBody] NurseUpdateDto dto)
        {
            var nurse = await _db.Nurses.FindAsync(id);
            if (nurse == null) return NotFound(new { message = "Nurse not found." });

            _mapper.Map(dto, nurse);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Nurse created successfully." });
        }

        /* Delete Nurse */
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNurse(int id)
        {
            var nurse = await _db.Nurses.FindAsync(id);
            if (nurse == null) return NotFound(new { message = "Nurse not found." });

            _db.Nurses.Remove(nurse);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Nurse deleted successfully." });

        }
    }
}
