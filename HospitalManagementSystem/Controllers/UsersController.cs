using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.User;
using HospitalManagementSystem.Data.Mappers;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public UsersController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        //   ########################   GET  ########################  //
        // GET: api/users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var users = await _db.Users.Include(u => u.Role).Where(u => u.IsActive == true).ToListAsync();
            if (users.Count > 0)
            {
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
            }
            return NotFound(new
            {
                Message = "User Not Found :(",
                ErrorCode = 404,
                Timestamp = DateTime.UtcNow
            });
        }

        // GET: api/users/deactive

        [HttpGet("deactive")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllDeactive()
        {
            var users = await _db.Users.Include(u => u.Role).Where(u => u.IsActive == false).ToListAsync();
            if (users.Count > 0)
            {
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
            }
            return NotFound(new
            {
                Message = "User Not Found :(",
                ErrorCode = 404,
                Timestamp = DateTime.UtcNow
            });
        }

        // GET: api/users/5

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUser(int id)
        {
            var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            return NotFound(new
            {
                Message = "User Not Found :(",
                ErrorCode = 404,
                Timestamp = DateTime.UtcNow
            });
        }

        // GET: api/users/5/isActive

        [HttpGet("{id}/isActive")]
        public async Task<ActionResult<bool>> IsActive(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User Not Found :(",
                    ErrorCode = 404,
                    Timestamp = DateTime.UtcNow
                });
            }
            return user.IsActive;
        }

        //   ########################   POST  ########################  //
        // POST: api/users

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (userDto != null)
            {
                var user = _mapper.Map<User>(userDto);
                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == user.UserId);

                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userReadDto);
            }
            return BadRequest("Error in creation this user");
        }


        //   ########################   PUT  ########################  //
        // Put: api/users/4

        [HttpPut("{id}")]
        public async Task<ActionResult<UserReadDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => (u.UserId == id) && (u.IsActive == true));
            if (user == null)
            return NotFound(new
            {
                Message = "User Not Found :(",
                ErrorCode = 404,
                Timestamp = DateTime.UtcNow
            });

            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                user.FirstName = dto.FirstName.Trim();
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                user.LastName = dto.LastName.Trim();
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.Phone))
            {
                user.Phone = dto.Phone.Trim();
                isUpdated = true;
            }

            if (!isUpdated)
                return BadRequest("No fields were edited.");

            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserReadDto>(user));
        }

        // Put: api/users/4/email
        [HttpPut("{id}/email")]
        public async Task<ActionResult<UserReadDto>> UpdateEmail(int id, [FromBody] UpdateEmailDto emailDto)
        {
            var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => (u.UserId == id) && (u.IsActive == true));
            if (user == null)
                return NotFound(new
                {
                    Message = "User Not Found :(",
                    ErrorCode = 404,
                    Timestamp = DateTime.UtcNow
                });
            if(string.IsNullOrWhiteSpace(emailDto.Email))
            {
                return BadRequest("No email is edited.");
            }
            user.Email = emailDto.Email;
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserReadDto>(user));
        }

        // Put: api/users/4/password
        [HttpPut("{id}/password")]
        public async Task<ActionResult<UserReadDto>> UpdatePassword(int id, [FromBody] UpdatePasswordDto dto)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);

            if (user == null)
                return NotFound(new
                {
                    Message = "User Not Found :(",
                    ErrorCode = 404,
                    Timestamp = DateTime.UtcNow
                });

            if (string.IsNullOrWhiteSpace(dto.OldPassword) || string.IsNullOrWhiteSpace(dto.NewPassword))
                return BadRequest("Both old and new password are required.");

            //bool isOldPasswordCorrect = BCrypt.Verify(dto.OldPassword, user.PasswordHash);

            if (dto.OldPassword != user.PasswordHash) 
                return BadRequest("Old password is incorrect.");

            user.PasswordHash = dto.NewPassword;
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<UserReadDto>(user));
        }


        //   ########################   DELETE  ########################  //
        // Delete: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserReadDto>> DeleteUser(int id)
        {
            var user = await _db.Users
                .Include(u => u.Doctor)
                .Include(u => u.Nurse)
                .Include(u => u.Patient)
                .Include(u => u.Receptionist)
                .FirstOrDefaultAsync(u => (u.UserId == id) && (u.IsActive == true));

            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User Not Found :(",
                    ErrorCode = 404,
                    Timestamp = DateTime.UtcNow
                });
            }

            if (user.Doctor != null)
            {
                user.Doctor.IsActive = false;
            }
            else if (user.Nurse != null)
            {
                user.Nurse.IsActive = false;
            }
            else if (user.Patient != null)
            {
                user.Patient.IsActive = false;
            }
            else if (user.Receptionist != null)
            {
                user.Receptionist.IsActive = false;
            }
            else
            {
                // Nothing
            }

            user.IsActive = false;
            await _db.SaveChangesAsync();
            //return Ok(_mapper.Map<UserReadDto>(user));
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserReadDto>(user));

        }




    }
}
