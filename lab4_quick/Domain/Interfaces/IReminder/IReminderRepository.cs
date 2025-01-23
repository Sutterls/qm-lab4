using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IReminder
{

    public interface IReminderRepository
    {
        Task<Reminder?> GetByIdAsync(Guid id);
        Task<IEnumerable<Reminder>> GetAllByMeetingIdAsync(Guid meetingId);
        Task AddAsync(Reminder reminder);
        Task UpdateAsync(Reminder reminder);
        Task DeleteAsync(Guid id);
    }

}
