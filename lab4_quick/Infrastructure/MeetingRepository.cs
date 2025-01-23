using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IMeeting;
using Microsoft.EntityFrameworkCore;

namespace lab4_quick.Infrastructure
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly QuickMeetingsContext _context;

        public MeetingRepository(QuickMeetingsContext context)
        {
            _context = context;
        }

        public async Task<Meeting?> GetByIdAsync(Guid id)
        {
            return await _context.Meeting.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Meeting>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Meeting.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(Meeting meeting)
        {
            await _context.Meeting.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            _context.Meeting.Update(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var meeting = await GetByIdAsync(id);
            if (meeting != null)
            {
                _context.Meeting.Remove(meeting);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Meeting>> GetUpcomingByUserIdAsync(Guid userId)
        {
            return await _context.Meeting
                .Where(m => m.UserId == userId && m.StartTime > DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
