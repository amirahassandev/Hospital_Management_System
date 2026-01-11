using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Dto.Department;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;
        public DepartmentController(HospitalDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        /* Get All Departments */
        // GET: api/Department/get-all-departments
        [HttpGet("get-all-departments")]
        public async Task<ActionResult<IEnumerable<DepartmentReadDto>>> GetAllDepartments()
        {
            var departments = await _db.Departments
                .Include(d => d.Nurses)
                .Include(d => d.Rooms)
                .AsNoTracking()
                .ToListAsync();
            // Implementation for getting all departments
            return Ok(_mapper.Map<IEnumerable<DepartmentReadDto>>(departments));
        }
        /* Get Department By Id */
        // GET: api/Department/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentReadDto>> GetDepartmentById(int id)
        {
            var department = await _db.Departments
                .Include(d => d.Nurses)
                .Include(d => d.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound(new { Message = "Department Not Found :(" });
            }
            return Ok(_mapper.Map<DepartmentReadDto>(department));
        }
        /* Create Department */
        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<DepartmentReadDto>> CreateDepartment([FromBody] DepartmentCreateDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            var createdDepartment = _mapper.Map<DepartmentReadDto>(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentId }, createdDepartment);
        }
        /* Update Department */
        // PUT: api/Department/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto departmentDto)
        {
            var department = await _db.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { Message = "Department Not Found :(" });
            }
            _mapper.Map(departmentDto, department);
            await _db.SaveChangesAsync();
            return Ok(new { Message = "Department Updated Successfully :)" });
        }
        /* Delete Department */
        // DELETE: api/Department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            {
                var department = await _db.Departments
                    .Include(d => d.Nurses)
                    .Include(d => d.Rooms)
                    .FirstOrDefaultAsync(d => d.DepartmentId == id);

                if (department == null)
                    return NotFound();

                if (department.Nurses.Any() || department.Rooms.Any())
                    return BadRequest("Cannot delete department with related nurses or rooms.");

                _db.Departments.Remove(department);
                await _db.SaveChangesAsync();

                return Ok(new { message = "Department deleted successfully." });
            }
        }
    }
}
