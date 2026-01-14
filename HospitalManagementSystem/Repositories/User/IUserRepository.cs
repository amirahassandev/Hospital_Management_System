using HospitalManagementSystem.Data.Models;
namespace HospitalManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(bool isActive = true);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByIdIncludeRoleAsync(int id);
        Task<User?> AddAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<User?> DeleteAsync(User user);
        Task<bool> SaveChangesAsync();

    }
}
