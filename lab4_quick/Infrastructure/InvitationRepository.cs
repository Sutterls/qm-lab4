using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IInvitation;
using Microsoft.EntityFrameworkCore;


namespace lab4_quick.Infrastructure
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly QuickMeetingsContext _context;

        public InvitationRepository(QuickMeetingsContext context)
        {
            _context = context;
        }

        public async Task<Invitation?> GetByIdAsync(Guid id)
        {
            return await _context.Invitation.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Invitation>> GetInvitationsByMeetingIdAsync(Guid meetingId)
        {
            return await _context.Invitation.Where(i => i.MeetingId == meetingId).ToListAsync();
        }


        public async Task AddAsync(Invitation invitation)
        {
            await _context.Invitation.AddAsync(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invitation invitation)
        {
            _context.Invitation.Update(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var invitation = await GetByIdAsync(id);
            if (invitation != null)
            {
                _context.Invitation.Remove(invitation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
