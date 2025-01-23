using lab4_quick.Domain.Entities;

namespace lab4_quick.Domain.Interfaces.IInvitation
{


    public interface IInvitationService
    {
        Task<Invitation?> GetInvitationByIdAsync(Guid id);
        Task CreateInvitationAsync(Guid meetingId, string inviteeEmail); 
        Task UpdateInvitationAsync(Guid id, Invitation.InvitationStatus status); 
        Task DeleteInvitationAsync(Guid id);
        Task<IEnumerable<Invitation>> GetInvitationsByMeetingIdAsync(Guid meetingId);
    }

}
