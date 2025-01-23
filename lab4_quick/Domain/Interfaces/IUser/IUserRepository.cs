using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
