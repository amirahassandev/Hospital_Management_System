using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public RoomController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        /*Get All Rooms */
        // GET: api/Room/getAllRooms
        [HttpGet("getAllRooms")]
        public async Task<ActionResult<IEnumerable<RoomReadDto>>> GetAllRooms()
        {
            var rooms = await _db.Rooms.
                Include(r => r.Department).
                Include(r => r.RoomStatus).
                Include(r => r.Admissions)
                .AsNoTracking()
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RoomReadDto>>(rooms));
        }
        /* Get Room By Id */
        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomReadDto>> GetRoomById(int id)
        {
            var room = await _db.Rooms
                .Include(r => r.Department)
                .Include(r => r.RoomStatus)
                .Include(r => r.Admissions)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoomId == id);
            if (room == null)
            {
                return NotFound(new { Message = "Room Not Found :(" });
            }
            return Ok(_mapper.Map<RoomReadDto>(room));
        }
        /* Create Room */
        // POST: api/Room/createRoom
        [HttpPost("createRoom")]
        public async Task<ActionResult<RoomReadDto>> CreateRoom([FromBody] RoomCreateDto roomDto)
        {
            var departmentExists = await _db.Departments.AnyAsync(d => d.DepartmentId == roomDto.DepartmentId);
            if (!departmentExists)
                return BadRequest("Department does not exist.");
            var roomStatusExists = await _db.RoomStatuses.AnyAsync(rs => rs.RoomStatusId == roomDto.RoomStatusId);
            if (!roomStatusExists)
                return BadRequest("Room Status does not exist.");
            var room = _mapper.Map<HospitalManagementSystem.Models.Room>(roomDto);
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            await _db.Entry(room).Reference(r => r.Department).LoadAsync();
            await _db.Entry(room).Reference(r => r.RoomStatus).LoadAsync();

            return CreatedAtAction(nameof(GetRoomById),
                new { id = room.RoomId },
                _mapper.Map<RoomReadDto>(room));
        }
        // PUT: api/rooms/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto dto)
        {
            var room = await _db.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            _mapper.Map(dto, room);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Room updated successfully." });
        }

        // DELETE: api/rooms/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _db.Rooms
                .Include(r => r.Admissions)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            if (room.Admissions.Any())
                return BadRequest("Cannot delete room with admissions.");

            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Room deleted successfully." });
        }
    }
}
