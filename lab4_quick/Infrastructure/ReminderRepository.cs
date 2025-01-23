using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IReminder;
using Microsoft.EntityFrameworkCore;

namespace lab4_quick.Infrastructure
{

    public class ReminderRepository : IReminderRepository
    {
        private readonly QuickMeetingsContext _context;

        public ReminderRepository(QuickMeetingsContext context)
        {
            _context = context;
        }

        public async Task<Reminder?> GetByIdAsync(Guid id)
        {
            return await _context.Reminder.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reminder>> GetAllByMeetingIdAsync(Guid meetingId)
        {
            return await _context.Reminder.Where(r => r.MeetingId == meetingId).ToListAsync();
        }

        public async Task AddAsync(Reminder reminder)
        {
            await _context.Reminder.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Reminder.Update(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reminder = await GetByIdAsync(id);
            if (reminder != null)
            {
                _context.Reminder.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }

}
