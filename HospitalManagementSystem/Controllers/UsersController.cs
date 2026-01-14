using HospitalManagementSystem.Dto.User;
using HospitalManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllUsersAsync(true);
            return users.Any() ? Ok(users) : NotFound(new { Message = "User Not Found :(", ErrorCode = 404, Timestamp = DateTime.UtcNow });
        }

        [HttpGet("deactive")]
        public async Task<IActionResult> GetAllDeactive()
        {
            var users = await _service.GetAllUsersAsync(false);
            return users.Any() ? Ok(users) : NotFound(new { Message = "User Not Found :(", ErrorCode = 404, Timestamp = DateTime.UtcNow });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound(new { Message = "User Not Found :(", ErrorCode = 404, Timestamp = DateTime.UtcNow });
        }

        [HttpGet("{id}/isActive")]
        public async Task<IActionResult> IsActive(int id)
        {
            try
            {
                var result = await _service.IsActiveAsync(id);
                return Ok(result);
            }
            catch
            {
                return NotFound(new { Message = "User Not Found :(", ErrorCode = 404, Timestamp = DateTime.UtcNow });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdUser = await _service.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _service.UpdateUserAsync(id, dto);
            return updatedUser != null ? Ok(updatedUser) : BadRequest("No fields were edited or user not found.");
        }

        [HttpPut("{id}/email")]
        public async Task<IActionResult> UpdateEmail(int id, [FromBody] UpdateEmailDto dto)
        {
            var updatedUser = await _service.UpdateEmailAsync(id, dto);
            return updatedUser != null ? Ok(updatedUser) : BadRequest("No email edited or user not found.");
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordDto dto)
        {
            var updatedUser = await _service.UpdatePasswordAsync(id, dto);
            return updatedUser != null ? Ok(updatedUser) : BadRequest("Password not updated or incorrect old password.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _service.DeleteUserAsync(id);
            return deletedUser != null ? Ok(deletedUser) : NotFound(new { Message = "User Not Found :(", ErrorCode = 404, Timestamp = DateTime.UtcNow });
        }
    }
}
