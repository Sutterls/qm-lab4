using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IUser;

namespace lab4_quick.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task CreateUserAsync(string username, string passwordHash, string email)
        {
            var user = new User
            {
                Id = Guid.NewGuid(), 
                Username = username,
                PasswordHash = passwordHash,
                Email = email
            };

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(Guid id, string username, string passwordHash, string email)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            user.Username = username;
            user.PasswordHash = passwordHash;
            user.Email = email;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
