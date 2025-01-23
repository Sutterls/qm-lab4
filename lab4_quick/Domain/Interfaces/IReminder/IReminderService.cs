using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IReminder
{
    public interface IReminderService
    {
        Task<Reminder?> GetReminderByIdAsync(Guid id);
        Task<IEnumerable<Reminder>> GetAllRemindersByMeetingIdAsync(Guid meetingId);
        Task AddReminderAsync(Guid meetingId, DateTime reminderTime);
        Task UpdateReminderAsync(Guid id, DateTime reminderTime);
        Task DeleteReminderAsync(Guid id);
    }
}
