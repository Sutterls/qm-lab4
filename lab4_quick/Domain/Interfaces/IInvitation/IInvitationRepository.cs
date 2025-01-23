using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IInvitation
{

    public interface IInvitationRepository
    {
        Task<Invitation?> GetByIdAsync(Guid id);
        Task AddAsync(Invitation invitation);
        Task UpdateAsync(Invitation invitation);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Invitation>> GetInvitationsByMeetingIdAsync(Guid meetingId); 
    }

}
