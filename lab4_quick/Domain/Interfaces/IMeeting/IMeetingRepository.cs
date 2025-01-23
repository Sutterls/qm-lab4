using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IMeeting
{
    public interface IMeetingRepository
    {
        Task<Meeting?> GetByIdAsync(Guid id);
        Task<IEnumerable<Meeting>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Meeting meeting);
        Task UpdateAsync(Meeting meeting);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Meeting>> GetUpcomingByUserIdAsync(Guid userId);
    }
}
