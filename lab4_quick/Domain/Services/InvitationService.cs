
using lab4_quick.Domain.Entities;
using lab4_quick.Domain.Interfaces.IInvitation;

namespace lab4_quick.Domain.Services
{


    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<Invitation?> GetInvitationByIdAsync(Guid id)
        {
            return await _invitationRepository.GetByIdAsync(id);
        }

        public async Task CreateInvitationAsync(Guid meetingId, string inviteeEmail)
        {
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                MeetingId = meetingId,
                InviteeEmail = inviteeEmail,
                Status = Invitation.InvitationStatus.Pending 
            };

            await _invitationRepository.AddAsync(invitation);
        }

        public async Task UpdateInvitationAsync(Guid id, Invitation.InvitationStatus status)
        {
            var invitation = await _invitationRepository.GetByIdAsync(id);
            if (invitation == null)
            {
                throw new Exception("Приглашение не найдено");
            }

            invitation.Status = status; 

            await _invitationRepository.UpdateAsync(invitation);
        }

        public async Task DeleteInvitationAsync(Guid id)
        {
            await _invitationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Invitation>> GetInvitationsByMeetingIdAsync(Guid meetingId)
        {
            return await _invitationRepository.GetInvitationsByMeetingIdAsync(meetingId);
        }
    }

}