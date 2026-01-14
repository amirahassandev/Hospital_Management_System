using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalDbContext _db;

        public UserRepository(HospitalDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool isActive = true)
        {
            return await _db.Users
                            .Include(u => u.Role)
                            .Where(u => u.IsActive == isActive)
                            .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users
                            .Include(u => u.Role)
                            .Include(u => u.Doctor)
                            .Include(u => u.Nurse)
                            .Include(u => u.Patient)
                            .Include(u => u.Receptionist)
                            .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByIdIncludeRoleAsync(int id)
        {
            return await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return await GetByIdIncludeRoleAsync(user.UserId);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return await GetByIdIncludeRoleAsync(user.UserId);
        }

        public async Task<User?> DeleteAsync(User user)
        {
            user.IsActive = false;

            if (user.Doctor != null) user.Doctor.IsActive = false;
            if (user.Nurse != null) user.Nurse.IsActive = false;
            if (user.Patient != null) user.Patient.IsActive = false;
            if (user.Receptionist != null) user.Receptionist.IsActive = false;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return await GetByIdIncludeRoleAsync(user.UserId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
