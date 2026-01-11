using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.Receptionist;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionistsController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public ReceptionistsController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        // GET: api/receptionists/getall
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<ReceptionistReadDto>>> GetAll()
        {
            var receptionists = await _db.Receptionists
                .Include(r => r.User)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<ReceptionistReadDto>>(receptionists));
        }

        // GET: api/receptionists/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReceptionistReadDto>> GetById(int id)
        {
            var receptionist = await _db.Receptionists
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReceptionistId == id);

            if (receptionist == null)
                return NotFound();

            return Ok(_mapper.Map<ReceptionistReadDto>(receptionist));
        }

        // POST: api/receptionists/create
        [HttpPost("create")]
        public async Task<ActionResult<ReceptionistReadDto>> Create(
            [FromBody] ReceptionistCreateDto dto)
        {
            var userExists = await _db.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
                return BadRequest("User does not exist.");

            var alreadyReceptionist =
                await _db.Receptionists.AnyAsync(r => r.UserId == dto.UserId);
            if (alreadyReceptionist)
                return BadRequest("User is already a receptionist.");

            var receptionist = _mapper.Map<Receptionist>(dto);
            _db.Receptionists.Add(receptionist);
            await _db.SaveChangesAsync();

            await _db.Entry(receptionist).Reference(r => r.User).LoadAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = receptionist.ReceptionistId },
                _mapper.Map<ReceptionistReadDto>(receptionist));
        }

        // PUT: api/receptionists/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ReceptionistUpdateDto dto)
        {
            var receptionist = await _db.Receptionists.FindAsync(id);
            if (receptionist == null)
                return NotFound();

            _mapper.Map(dto, receptionist);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/receptionists/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var receptionist = await _db.Receptionists.FindAsync(id);
            if (receptionist == null)
                return NotFound();

            _db.Receptionists.Remove(receptionist);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Receptionist deleted successfully." });
        }
    }
}
