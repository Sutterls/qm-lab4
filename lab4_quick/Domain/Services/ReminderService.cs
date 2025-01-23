using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IReminder;

namespace lab4_quick.Domain.Services
{

    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<Reminder?> GetReminderByIdAsync(Guid id)
        {
            return await _reminderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reminder>> GetAllRemindersByMeetingIdAsync(Guid meetingId)
        {
            return await _reminderRepository.GetAllByMeetingIdAsync(meetingId);
        }

        public async Task AddReminderAsync(Guid meetingId, DateTime reminderTime)
        {
            var reminder = new Reminder
            {
                Id = Guid.NewGuid(), 
                MeetingId = meetingId,
                ReminderTime = reminderTime
            };

            await _reminderRepository.AddAsync(reminder);
        }

        public async Task UpdateReminderAsync(Guid id, DateTime reminderTime)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);
            if (reminder == null)
            {
                throw new Exception("Напоминание не найдено");
            }

            reminder.ReminderTime = reminderTime; 

            await _reminderRepository.UpdateAsync(reminder);
        }

        public async Task DeleteReminderAsync(Guid id)
        {
            await _reminderRepository.DeleteAsync(id);
        }
    }

}
