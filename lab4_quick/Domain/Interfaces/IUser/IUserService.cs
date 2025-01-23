using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IUser
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id); 
        Task<IEnumerable<User>> GetAllUsersAsync(); 
        Task CreateUserAsync(string username, string passwordHash, string email); 
        Task UpdateUserAsync(Guid id, string username, string passwordHash, string email);
        Task DeleteUserAsync(Guid id); 
    }
}
