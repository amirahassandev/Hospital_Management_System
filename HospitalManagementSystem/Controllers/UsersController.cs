using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.User;
using HospitalManagementSystem.Data.Mappers;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly HospitalDbContext _db;
        public UsersController(HospitalDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> getAll()
        {
            List<User> users = _db.Users.Include(u => u.Role).ToList();
            if (users.Count > 0)
            {
                return Ok(new { Message = UserMapper.ConvertUsersToUsersDto(users) });
            }
            return NotFound(new { Message = "Not Found Any User :(" });
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> getUser(int id) 
        {
            User user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
            if(user != null)
            {
                return Ok(new { Message = UserMapper.ConvertUserToUserDto(user) });
            }
            return NotFound(new { Message = "User Not Found :(" });
        }

        //[HttpDelete("{id}")]
        //public ActionResult<UserDto> DeleteUser(int id)
        //{
        //    User user = _db.Users.FirstOrDefault(u => u.UserId == id);
        //    if(user != null)
        //    {
        //        // remove object first (doctor, nurse, ...)
        //        _db.Users.Remove(user);
        //        _db.SaveChanges();
        //        return Ok(new { Message = UserMapper.ConvertUserToUserDto(user)});
        //    }
        //    return NotFound(new { Message = "User Not Found :(" });
        //}

        [HttpPost]
        public ActionResult<UserDto> CreateUser([FromBody] UserWithPasswordDto userDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (userDto != null)
            {
                User user = UserMapper.ConvertUserWithPasswordDtoToUserEntity(userDto);
                _db.Users.Add(user);
                _db.SaveChanges();
                return Ok(new { Message = UserMapper.ConvertUserToUserDto(user)});
            }
            return BadRequest("Error in creation this user");
        }

    }
}
